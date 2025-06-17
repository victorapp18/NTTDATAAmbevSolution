using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using NTTDATAAmbev.Application.DTOs;
using NTTDATAAmbev.Application.Services;
using NTTDATAAmbev.Domain.Entities;
using NTTDATAAmbev.Domain.Interfaces;

namespace NTTDATAAmbev.Application.Tests
{
    public class SaleServiceTests
    {
        private readonly Mock<ISaleRepository> _repoMock;
        private readonly SaleService _service;

        public SaleServiceTests()
        {
            _repoMock = new Mock<ISaleRepository>();
            _service = new SaleService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale
                {
                    Id = Guid.NewGuid(),
                    SaleNumber = "S1",
                    Date = DateTime.UtcNow,
                    Cancelled = false,
                    Items = new List<SaleItem>
                    {
                        new SaleItem
                        {
                            Id = Guid.NewGuid(),
                            ProductId = Guid.NewGuid(),
                            ProductName = "P1",
                            Quantity = 2,
                            UnitPrice = 10m,
                            Discount = 0m,
                            Total = 20m,
                            Cancelled = false
                        }
                    }
                }
            };
            _repoMock.Setup(r => r.GetAllAsync())
                     .ReturnsAsync(sales);

            // Act
            var dtos = await _service.GetAllAsync();

            // Assert
            Assert.Single(dtos);
            var dto = dtos.First();
            Assert.Equal("S1", dto.SaleNumber);
            Assert.Equal(20m, dto.TotalAmount);
            Assert.False(dto.Cancelled);
            Assert.Single(dto.Items);
            Assert.Equal("P1", dto.Items[0].ProductName);
        }

        [Fact]
        public async Task GetByIdAsync_NotFound_ReturnsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id))
                     .ReturnsAsync((Sale?)null);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_Found_ReturnsDto()
        {
            // Arrange
            var id = Guid.NewGuid();
            var sale = new Sale
            {
                Id = id,
                SaleNumber = "S2",
                Date = DateTime.UtcNow,
                Cancelled = false,
                Items = new List<SaleItem>()
            };
            _repoMock.Setup(r => r.GetByIdAsync(id))
                     .ReturnsAsync(sale);

            // Act
            var dto = await _service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(dto);
            Assert.Equal("S2", dto!.SaleNumber);
            Assert.Equal(id, dto.Id);
        }

        [Fact]
        public async Task CreateAsync_NullDto_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAsync(null!));
        }

        [Fact]
        public async Task CreateAsync_ValidDto_CallsAddAndReturnsId()
        {
            // Arrange
            var dto = new SaleDto
            {
                SaleNumber = "S3",
                Date = DateTime.UtcNow,
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "P2",
                        Quantity = 1,
                        UnitPrice = 5m,
                        Discount = 0m,
                        Total = 5m,
                        Cancelled = false
                    }
                }
            };

            Sale? captured = null;
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Sale>()))
                     .Callback<Sale>(s => captured = s)
                     .Returns(Task.CompletedTask);

            // Act
            var newId = await _service.CreateAsync(dto);

            // Assert
            Assert.NotEqual(Guid.Empty, newId);
            Assert.NotNull(captured);
            Assert.Equal(dto.SaleNumber, captured!.SaleNumber);
            Assert.Equal(5m, captured.TotalAmount);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
        }

        [Fact]
        public async Task CancelAsync_NotFound_ReturnsFalse()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id))
                     .ReturnsAsync((Sale?)null);

            // Act
            var result = await _service.CancelAsync(id);

            // Assert
            Assert.False(result);
            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Sale>()), Times.Never);
        }

        [Fact]
        public async Task CancelAsync_Found_SetsCancelledAndCallsUpdate()
        {
            // Arrange
            var id = Guid.NewGuid();
            var sale = new Sale
            {
                Id = id,
                SaleNumber = "S4",
                Date = DateTime.UtcNow,
                Cancelled = false,
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        ProductName = "P3",
                        Quantity = 3,
                        UnitPrice = 2m,
                        Discount = 0m,
                        Total = 6m,
                        Cancelled = false
                    }
                }
            };
            _repoMock.Setup(r => r.GetByIdAsync(id))
                     .ReturnsAsync(sale);
            _repoMock.Setup(r => r.UpdateAsync(sale))
                     .Returns(Task.CompletedTask);

            // Act
            var result = await _service.CancelAsync(id);

            // Assert
            Assert.True(result);
            Assert.True(sale.Cancelled);
            Assert.All(sale.Items, i => Assert.True(i.Cancelled));
            _repoMock.Verify(r => r.UpdateAsync(sale), Times.Once);
        }
    }
}

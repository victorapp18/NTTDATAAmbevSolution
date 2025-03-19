using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using NTTDATAmbevSolution.Application.DTOs;
using NTTDATAmbevSolution.Application.Services;
using NTTDATAmbevSolution.Domain.Entities;
using NTTDATAmbevSolution.Domain.Interfaces;

namespace NTTDATAmbevSolution.Tests.Services
{
    public class SaleServiceTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly SaleService _saleService;

        public SaleServiceTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _saleRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Sale>()); // Retorna uma lista vazia para o mock
            _saleService = new SaleService(_saleRepositoryMock.Object);
        }

        [Fact]
        public void CreateSale_ShouldNotApplyDiscount_WhenQuantityIsLessThan4()
        {
            // Arrange
            var saleDto = new SaleDto
            {
                Date = DateTime.Now,
                Customer = "Cliente A",
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto { ProductId = 1, Quantity = 2, UnitPrice = 100 } // Sem desconto
                }
            };

            // Act
            var sale = _saleService.CreateSale(saleDto);

            // Assert
            Assert.Equal(200, sale.TotalAmount); // (2 * 100) - 0
            Assert.Equal(0, sale.Items.First().Discount);
        }

        [Fact]
        public void CreateSale_ShouldApply10PercentDiscount_WhenQuantityIsBetween4And9()
        {
            // Arrange
            var saleDto = new SaleDto
            {
                Date = DateTime.Now,
                Customer = "Cliente B",
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto { ProductId = 1, Quantity = 5, UnitPrice = 100 } // 10% de desconto
                }
            };

            // Act
            var sale = _saleService.CreateSale(saleDto);

            // Assert
            Assert.Equal(450, sale.TotalAmount); // (5 * 100) - (5 * 100 * 10%)
            Assert.Equal(50, sale.Items.First().Discount); // 5 * 100 * 10%
        }

        [Fact]
        public void CreateSale_ShouldApply20PercentDiscount_WhenQuantityIsBetween10And20()
        {
            // Arrange
            var saleDto = new SaleDto
            {
                Date = DateTime.Now,
                Customer = "Cliente C",
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto { ProductId = 1, Quantity = 15, UnitPrice = 50 } // 20% de desconto
                }
            };

            // Act
            var sale = _saleService.CreateSale(saleDto);

            // Assert
            Assert.Equal(600, sale.TotalAmount); // (15 * 50) - (15 * 50 * 20%)
            Assert.Equal(150, sale.Items.First().Discount); // 15 * 50 * 20%
        }

        [Fact]
        public void CreateSale_ShouldThrowException_WhenQuantityExceedsLimit()
        {
            // Arrange
            var saleDto = new SaleDto
            {
                Date = DateTime.Now,
                Customer = "Cliente Teste",
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto { ProductId = 2, Quantity = 25, UnitPrice = 30 } // Excede o limite de 20 unidades
                }
            };

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _saleService.CreateSale(saleDto));
            Assert.Equal("Não pode vender mais que 20 unidades", exception.Message);
        }

        [Fact]
        public void CreateSale_ShouldAssignCorrectId_WhenNewSaleIsAdded()
        {
            // Arrange
            var existingSales = new List<Sale>
            {
                new Sale { Id = 1, Date = DateTime.Now, Customer = "Cliente X", Items = new List<SaleItem>() },
                new Sale { Id = 2, Date = DateTime.Now, Customer = "Cliente Y", Items = new List<SaleItem>() }
            };

            _saleRepositoryMock.Setup(repo => repo.GetAll()).Returns(existingSales);

            var saleDto = new SaleDto
            {
                Date = DateTime.Now,
                Customer = "Cliente Z",
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto { ProductId = 3, Quantity = 5, UnitPrice = 20 } // Deve receber ID 3
                }
            };

            // Act
            var sale = _saleService.CreateSale(saleDto);

            // Assert
            Assert.Equal(3, sale.Id); 
        }
    }
}

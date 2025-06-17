using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NTTDATAAmbev.Application.DTOs;
using NTTDATAAmbev.Application.Interfaces;
using NTTDATAAmbev.Domain.Entities;
using NTTDATAAmbev.Domain.Interfaces;

namespace NTTDATAAmbev.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        public SaleService(ISaleRepository saleRepository)
            => _saleRepository = saleRepository;

        public async Task<IEnumerable<SaleDto>> GetAllAsync()
        {
            var sales = await _saleRepository.GetAllAsync();
            return sales.Select(s => ToDto(s));
        }

        public async Task<SaleDto?> GetByIdAsync(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            return sale == null ? null : ToDto(sale);
        }

        public async Task<Guid> CreateAsync(SaleDto saleDto)
        {
            if (saleDto == null) throw new ArgumentNullException(nameof(saleDto));

            var sale = new NTTDATAAmbev.Domain.Entities.Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = saleDto.SaleNumber,
                Date = saleDto.Date,
                Cancelled = false,
                Items = saleDto.Items.Select(i => new SaleItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    Total = (i.UnitPrice * i.Quantity) - i.Discount,
                    Cancelled = false
                }).ToList()
            };
            sale.TotalAmount = sale.Items.Sum(i => i.Total);

            await _saleRepository.AddAsync(sale);
            return sale.Id;
        }

        public async Task<bool> CancelAsync(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null) return false;

            sale.Cancelled = true;
            sale.Items.ForEach(i => i.Cancelled = true);

            await _saleRepository.UpdateAsync(sale);
            return true;
        }

        private static SaleDto ToDto(NTTDATAAmbev.Domain.Entities.Sale sale)
            => new SaleDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                Date = sale.Date,
                TotalAmount = sale.TotalAmount,
                Cancelled = sale.Cancelled,
                Items = sale.Items.Select(i => new SaleItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    Total = i.Total,
                    Cancelled = i.Cancelled
                }).ToList()
            };
    }
}

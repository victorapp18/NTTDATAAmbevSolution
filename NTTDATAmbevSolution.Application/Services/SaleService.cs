using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NTTDATAAmbev.Application.DTOs;
using NTTDATAAmbev.Application.Interfaces;
using NTTDATAAmbev.Domain.Entities;
using NTTDATAAmbev.Domain.Interfaces;

namespace NTTDATAAmbev.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleService> _logger;

        public SaleService(ISaleRepository saleRepository, ILogger<SaleService> logger)
        {
            _saleRepository = saleRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<SaleDto>> GetAllAsync()
        {
            _logger.LogInformation("Buscando todas as vendas.");
            var sales = await _saleRepository.GetAllAsync();
            _logger.LogInformation("Total de vendas encontradas: {Count}", sales.Count());
            return sales.Select(ToDto);
        }

        public async Task<SaleDto?> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Buscando venda com ID: {SaleId}", id);
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                _logger.LogWarning("Venda com ID {SaleId} não encontrada.", id);
                return null;
            }
            _logger.LogInformation("Venda com ID {SaleId} encontrada.", id);
            return ToDto(sale);
        }

        public async Task<Guid> CreateAsync(SaleDto saleDto)
        {
            if (saleDto == null)
            {
                _logger.LogError("Tentativa de criação de venda com objeto nulo.");
                throw new ArgumentNullException(nameof(saleDto));
            }

            var saleId = Guid.NewGuid();
            _logger.LogInformation("Criando nova venda com ID: {SaleId}", saleId);

            var sale = new Sale
            {
                Id = saleId,
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

            _logger.LogInformation("Venda {SaleId} criada com sucesso. Total: {Total}", saleId, sale.TotalAmount);
            return sale.Id;
        }

        public async Task<bool> CancelAsync(Guid id)
        {
            _logger.LogInformation("Cancelando venda com ID: {SaleId}", id);
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                _logger.LogWarning("Tentativa de cancelar venda não encontrada com ID: {SaleId}", id);
                return false;
            }

            sale.Cancelled = true;
            sale.Items.ForEach(i => i.Cancelled = true);

            await _saleRepository.UpdateAsync(sale);
            _logger.LogInformation("Venda com ID {SaleId} cancelada com sucesso.", id);
            return true;
        }

        private static SaleDto ToDto(Sale sale)
        {
            var items = sale.Items.Select(i => new SaleItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                Total = i.Total,
                Cancelled = i.Cancelled
            }).ToList();

            return new SaleDto
            {
                Id = sale.Id,
                SaleNumber = sale.SaleNumber,
                Date = sale.Date,
                Items = items,
                TotalAmount = items.Sum(i => i.Total),
                Cancelled = sale.Cancelled
            };
        }
    }
}

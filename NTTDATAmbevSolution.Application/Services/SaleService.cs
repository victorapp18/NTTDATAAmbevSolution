using System;
using System.Collections.Generic;
using System.Linq;
using NTTDATAmbevSolution.Application.DTOs;
using NTTDATAmbevSolution.Application.Interfaces;
using NTTDATAmbevSolution.Domain.Entities;
using NTTDATAmbevSolution.Domain.Interfaces;

namespace NTTDATAmbevSolution.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public SaleDto CreateSale(SaleDto saleDto)
        {
            var sale = new Sale
            {
                Id = _saleRepository.GetAll().Count() + 1, // Pegando a contagem das vendas já cadastradas
                Date = saleDto.Date,
                Customer = saleDto.Customer,
                Items = saleDto.Items.Select(item => new SaleItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = CalculateDiscount(item.Quantity, item.UnitPrice)
                }).ToList()
            };

            // 🔴 Atualizar o total com base nos itens
            sale.TotalAmount = sale.Items.Sum(item => (item.Quantity * item.UnitPrice) - item.Discount);

            _saleRepository.Add(sale);

            return new SaleDto
            {
                Id = sale.Id,
                Date = sale.Date,
                Customer = sale.Customer,
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(i => new SaleItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount
                }).ToList()
            };
        }

        public SaleDto GetSaleById(int id)
        {
            var sale = _saleRepository.GetById(id);
            if (sale == null) return null;

            return new SaleDto
            {
                Id = sale.Id,
                Date = sale.Date,
                Customer = sale.Customer,
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(i => new SaleItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount
                }).ToList()
            };
        }

        public IEnumerable<SaleDto> GetAllSales()
        {
            return _saleRepository.GetAll().Select(sale => new SaleDto
            {
                Id = sale.Id,
                Date = sale.Date,
                Customer = sale.Customer,
                TotalAmount = sale.TotalAmount,
                Items = sale.Items.Select(i => new SaleItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount
                }).ToList()
            });
        }

        public void UpdateSale(SaleDto saleDto)
        {
            var sale = _saleRepository.GetById(saleDto.Id);
            if (sale == null) return;

            sale.Date = saleDto.Date;
            sale.Customer = saleDto.Customer;
            sale.Items = saleDto.Items.Select(i => new SaleItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = CalculateDiscount(i.Quantity, i.UnitPrice)
            }).ToList();

            sale.TotalAmount = sale.Items.Sum(i => (i.Quantity * i.UnitPrice) - i.Discount);

            _saleRepository.Update(sale);
        }

        public void DeleteSale(int id)
        {
            _saleRepository.Delete(id);
        }

        private decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity >= 10 && quantity <= 20)
                return unitPrice * quantity * 0.20m;
            if (quantity >= 4)
                return unitPrice * quantity * 0.10m;
            return 0;
        }
    }
}

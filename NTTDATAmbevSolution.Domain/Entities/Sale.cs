// Domain/Entities/Sale.cs
using System;
using System.Collections.Generic;

namespace NTTDATAAmbev.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public List<SaleItem> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public bool Cancelled { get; set; }
    }

    public class SaleItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public bool Cancelled { get; set; }
    }
}

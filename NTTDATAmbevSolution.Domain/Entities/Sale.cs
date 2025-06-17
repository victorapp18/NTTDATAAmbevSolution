using System;
using System.Collections.Generic;
using NTTDATAAmbev.Domain.Interfaces;

namespace NTTDATAAmbev.Domain.Entities
{
    public class Sale : IEntity
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public Branch Branch { get; set; }
        public List<SaleItem> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public bool Cancelled { get; set; }
    }

    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
    }

    public class Branch
    {
        public Guid BranchId { get; set; }
        public string Name { get; set; }
    }

    public class SaleItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public bool Cancelled { get; set; }
    }
}

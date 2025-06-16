using System;
using System.Collections.Generic;

namespace NTTDATAAmbev.Application.DTOs
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime Date { get; set; }
        public CustomerDto Customer { get; set; }
        public BranchDto Branch { get; set; }
        public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();
        public decimal TotalAmount { get; set; }
        public bool Cancelled { get; set; }
    }

    public class SaleItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public bool Cancelled { get; set; }
    }

    public class CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
    }

    public class BranchDto
    {
        public Guid BranchId { get; set; }
        public string Name { get; set; }
    }
}

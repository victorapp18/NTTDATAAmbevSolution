using System;
using System.Collections.Generic;

namespace NTTDATAAmbev.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        // External Identities (denormalized)
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
        public decimal TotalAmount { get; set; }
        public bool Cancelled { get; set; }
    }
}

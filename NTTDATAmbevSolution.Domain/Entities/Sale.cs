using NTTDATAmbevSolution.Domain.Enums;
using System;
using System.Collections.Generic;

namespace NTTDATAmbevSolution.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItem> Items { get; set; } = new();
        public SaleStatus Status { get; set; }

        public void CalculateTotal()
        {
            TotalAmount = 0;
            foreach (var item in Items)
            {
                TotalAmount += item.TotalPrice;
            }
        }

        public void CancelSale()
        {
            Status = SaleStatus.Cancelled;
        }
    }
}

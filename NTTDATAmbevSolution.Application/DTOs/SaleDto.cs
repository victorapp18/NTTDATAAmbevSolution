using System;
using System.Collections.Generic;

namespace NTTDATAmbevSolution.Application.DTOs
{
    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleItemDto> Items { get; set; }
    }

    public class SaleItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}

using System.Collections.Generic;
using NTTDATAmbevSolution.Application.DTOs;

namespace NTTDATAmbevSolution.Application.Interfaces
{
    public interface ISaleService
    {
        SaleDto CreateSale(SaleDto saleDto);
        SaleDto GetSaleById(int id);
        IEnumerable<SaleDto> GetAllSales();
        void UpdateSale(SaleDto saleDto);
        void DeleteSale(int id);
    }
}

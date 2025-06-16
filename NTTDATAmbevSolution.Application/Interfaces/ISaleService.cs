using NTTDATAAmbev.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTTDATAAmbev.Application.Interfaces
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleDto>> GetAllAsync();
        Task<SaleDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(SaleDto saleDto);
        Task<bool> CancelAsync(Guid id);
    }
}

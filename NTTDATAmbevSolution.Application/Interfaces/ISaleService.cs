// Application/Interfaces/ISaleService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NTTDATAAmbev.Application.DTOs;

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

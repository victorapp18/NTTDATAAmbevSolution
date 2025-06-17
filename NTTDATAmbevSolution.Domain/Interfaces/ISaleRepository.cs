using NTTDATAAmbev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTTDATAAmbev.Domain.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllAsync();
        Task<Sale?> GetByIdAsync(Guid id);
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(Guid id);
    }
}

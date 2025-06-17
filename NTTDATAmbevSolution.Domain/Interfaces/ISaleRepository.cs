// Domain/Interfaces/ISaleRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NTTDATAAmbev.Domain.Entities;

namespace NTTDATAAmbev.Domain.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllAsync();
        Task<Sale?> GetByIdAsync(Guid id);
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
    }
}

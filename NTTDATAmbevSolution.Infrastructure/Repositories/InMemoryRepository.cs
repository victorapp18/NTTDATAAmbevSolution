using NTTDATAAmbev.Domain.Entities;
using NTTDATAAmbev.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NTTDATAAmbev.Infrastructure.Repositories
{
    public class InMemorySaleRepository : ISaleRepository
    {
        private readonly ConcurrentDictionary<Guid, Sale> _sales = new();

        public Task AddAsync(Sale sale)
        {
            if (sale == null) throw new ArgumentNullException(nameof(sale));
            _sales.TryAdd(sale.Id, sale);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _sales.TryRemove(id, out _);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Sale>> GetAllAsync()
        {
            return Task.FromResult(_sales.Values.AsEnumerable());
        }

        public Task<Sale?> GetByIdAsync(Guid id)
        {
            _sales.TryGetValue(id, out var sale);
            return Task.FromResult(sale);
        }

        public Task UpdateAsync(Sale sale)
        {
            if (sale == null) throw new ArgumentNullException(nameof(sale));
            _sales.AddOrUpdate(sale.Id, sale, (key, oldValue) => sale);
            return Task.CompletedTask;
        }
    }
}

// Infrastructure/Repositories/InMemorySaleRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NTTDATAAmbev.Domain.Entities;
using NTTDATAAmbev.Domain.Interfaces;

namespace NTTDATAAmbev.Infrastructure.Repositories
{
    public class InMemorySaleRepository : ISaleRepository
    {
        private readonly List<Sale> _sales = new();

        public Task<IEnumerable<Sale>> GetAllAsync() =>
            Task.FromResult(_sales.AsEnumerable());

        public Task<Sale?> GetByIdAsync(Guid id) =>
            Task.FromResult(_sales.FirstOrDefault(s => s.Id == id));

        public Task AddAsync(Sale sale)
        {
            _sales.Add(sale);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Sale sale)
        {
            var index = _sales.FindIndex(s => s.Id == sale.Id);
            if (index != -1)
            {
                _sales[index] = sale;
            }
            return Task.CompletedTask;
        }
    }
}

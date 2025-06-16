using Microsoft.EntityFrameworkCore;
using NTTDATAAmbev.Domain.Entities;
using NTTDATAAmbev.Domain.Interfaces;
using NTTDATAAmbev.Infrastructure.Data;
using ServiceControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NTTDATAAmbev.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _context;

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale)
        {
            if (sale == null) throw new ArgumentNullException(nameof(sale));
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.Items)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(Sale sale)
        {
            if (sale == null) throw new ArgumentNullException(nameof(sale));
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }
    }
}

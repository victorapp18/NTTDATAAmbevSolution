using System.Collections.Generic;
using System.Linq;
using NTTDATAmbevSolution.Domain.Entities;
using NTTDATAmbevSolution.Domain.Interfaces;

namespace NTTDATAmbevSolution.Infrastructure.Repositories
{
    public class InMemorySaleRepository : ISaleRepository
    {
        private readonly List<Sale> _sales;

        public InMemorySaleRepository()
        {
            _sales = new List<Sale>(); 
        }

        public Sale Add(Sale sale)
        {
            sale.Id = _sales.Any() ? _sales.Max(s => s.Id) + 1 : 1;
            _sales.Add(sale);
            return sale;
        }

        public Sale GetById(int id)
        {
            return _sales.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Sale> GetAll()
        {
            return _sales;
        }

        public void Update(Sale sale)
        {
            var existingSale = GetById(sale.Id);
            if (existingSale != null)
            {
                existingSale.Date = sale.Date;
                existingSale.Customer = sale.Customer;
                existingSale.TotalAmount = sale.TotalAmount;
                existingSale.Items = sale.Items;
            }
        }

        public void Delete(int id)
        {
            var sale = GetById(id);
            if (sale != null)
            {
                _sales.Remove(sale);
            }
        }
    }
}

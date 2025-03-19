using System.Collections.Generic;
using NTTDATAmbevSolution.Domain.Entities;

namespace NTTDATAmbevSolution.Domain.Interfaces
{
    public interface ISaleRepository
    {
        Sale Add(Sale sale);
        Sale GetById(int id);
        IEnumerable<Sale> GetAll();
        void Update(Sale sale);
        void Delete(int id);
    }
}

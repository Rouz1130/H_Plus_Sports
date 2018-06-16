using H_Plus_Sports.Interfaces;
using H_Plus_Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Repositories
{
    public class SalespersonsRepository : ISalespersonsRepository
    {
        public Task<Salesperson> Add(Salesperson salesperson)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Salesperson> Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Salesperson> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Salesperson> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Salesperson> Update(Salesperson salesperson)
        {
            throw new NotImplementedException();
        }
    }
}

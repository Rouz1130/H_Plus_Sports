using H_Plus_Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Interfaces
{
    interface ISalespersonsRepository
    {
        Task<Salesperson> Add(Salesperson salesperson);
        IEnumerable<Salesperson> GetAll();
        Task<Salesperson> Find(int id);
        Task<Salesperson> Update(Salesperson salesperson);
        Task<Salesperson> Remove(int id);
        Task<bool> Exists(int id);
    }
}

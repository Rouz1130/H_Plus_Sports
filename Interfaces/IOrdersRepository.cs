using H_Plus_Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Interfaces
{
    interface IOrdersRepository
    {
        Task<Order> Add(Order order);
        IEnumerable<Order> GetAll();
        Task<Order> Find(int id);
        Task<Order> Update(Order order);
        Task<Order> Remove(int id);
        Task<bool> Exists(int id);
    }
}

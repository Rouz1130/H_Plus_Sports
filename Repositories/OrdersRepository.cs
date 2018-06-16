using H_Plus_Sports.Interfaces;
using H_Plus_Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        public Task<Order> Add(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Order> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}

﻿using H_Plus_Sports.Interfaces;
using H_Plus_Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        public Task<OrderItem> Add(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> Update(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }
    }
}

using H_Plus_Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Interfaces
{
   public interface IOrderItemRepository
    {
        Task<OrderItem> Add(OrderItem orderItem);

        IEnumerable<OrderItem> GetAll();

        Task<OrderItem> Find(int id);

        Task<OrderItem> Remove(int id);

        Task<OrderItem> Update(OrderItem orderItem);

        Task<bool> Exists(int id);
    }
}

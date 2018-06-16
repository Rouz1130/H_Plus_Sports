using H_Plus_Sports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product> Add(Product product);
        IEnumerable<Product> GetAll();
        Task<Product> Find(string id);
        Task<Product> Update(Product product);
        Task<Product> Remove(string id);
        Task<bool> Exists(string id);
    }
}

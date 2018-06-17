using H_Plus_Sports.Interfaces;
using H_Plus_Sports.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private H_Plus_SportsContext _context;

        private IDistributedCache _cache;

        public ProductRepository(H_Plus_SportsContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Product;
        }

        public async Task<Product> Add(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Taking a cached product in  Json fromat and converting
        /// it into C# object that will be returned back to the controller.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<Product> Find(string id)
        {
            var cachedProduct = await _cache.GetStringAsync(id);

            if (cachedProduct != null)
            {
                return JsonConvert.DeserializeObject<Product>(cachedProduct);
            }
            else
            {
                var dbProdcuts =  await _context.Product.SingleOrDefaultAsync(a => a.ProductId == id);

                await _cache.SetStringAsync(id, JsonConvert.SerializeObject(dbProdcuts));

                return dbProdcuts;
            }
            
        }

        public async Task<Product> Remove(string id)
        {
            var product = await _context.Product.SingleAsync(a => a.ProductId == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Exists(string id)
        {
            return await _context.Product.AnyAsync(e => e.ProductId == id);
        }
    }
}
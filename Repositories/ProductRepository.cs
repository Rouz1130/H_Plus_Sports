using H_Plus_Sports.Interfaces;
using H_Plus_Sports.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H_Plus_Sports.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private H_Plus_SportsContext _context;
        private IMemoryCache _cache;

        public ProductRepository(H_Plus_SportsContext context, IMemoryCache cache)
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
        /// Add In-Memory cacheing, For example cuts server time in half to reload page.
        /// Flow add To Startup Configure services, Add private variable in controller and add to method. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> Find(string id)
        {
            var cacheProduct = _cache.Get<Product>(id);

            if (cacheProduct != null)
            {
                return cacheProduct;
            }
            else
            {
                var dbProduct = await _context.Product.Include(product => product.OrderItem).SingleOrDefaultAsync(a => a.ProductId == id);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _cache.Set(dbProduct.ProductId, dbProduct, cacheEntryOptions);

                return dbProduct;
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
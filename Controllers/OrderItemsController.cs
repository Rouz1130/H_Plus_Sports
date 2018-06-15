using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H_Plus_Sports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/OrderItems")]
    public class OrderItemsController : Controller
    {
        private readonly H_Plus_SportsContext _context;

        public OrderItemsController(H_Plus_SportsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This is to check if OrderItem exists for PUT method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool OrderItemExists(int id)
        {
            return _context.OrderItem.Any(e => e.OrderItemId == id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<OrderItem>))]
        public IActionResult GetOrderItem()
        {
            return new ObjectResult(_context.OrderItem);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderItem = await _context.OrderItem.SingleOrDefaultAsync(m => m.OrderItemId == id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpPost]
        [Produces(typeof(OrderItem))]
        public async Task<IActionResult> PostOrderItem([FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItem", new { id = orderItem.OrderItemId }, orderItem);
        }

        [HttpPut("{id}")]
        [Produces(typeof(OrderItem))]
        public async Task<IActionResult> PutOrderItem([FromRoute] int id, [FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderItem.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(orderItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpDelete("{id}")]
        [Produces(typeof(OrderItem))]
        public async Task<IActionResult> DeleteOrderItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderItem = await _context.OrderItem.SingleOrDefaultAsync(m => m.OrderItemId == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItem.Remove(orderItem);
            await _context.SaveChangesAsync();

            return Ok(orderItem);
        }
    }
}
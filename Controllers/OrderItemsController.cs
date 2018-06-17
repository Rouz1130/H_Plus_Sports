using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using H_Plus_Sports.Interfaces;
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
        private readonly IOrderItemRepository _orderItems;

        public OrderItemsController(IOrderItemRepository orderItems)
        {
            _orderItems = orderItems;
        }

        /// <summary>
        /// This is to check if OrderItem exists for PUT method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<bool> OrderItemExists(int id)
        {
            return await _orderItems.Exists(id);
           
        }

        [HttpGet]
        [Produces(typeof(DbSet<OrderItem>))]
        public IActionResult GetOrderItem()
        {
            return new ObjectResult(_orderItems.GetAll());
        }

        [HttpGet("{id}")]
        [Produces(typeof(OrderItem))]
        public async Task<IActionResult> GetOrderItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderItem = await _orderItems.Find(id);

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

            await _orderItems.Add(orderItem);

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

            try
            {
                await _orderItems.Update(orderItem);
                return Ok(orderItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
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

            if (! await OrderItemExists(id))
            {
                return NotFound();
            }

            await _orderItems.Remove(id);

            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using H_Plus_Sports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace H_Plus_Sports.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
       ///<summary>
       ///Constructor. Private variable contain reference to the Dbcontext our actions can use. 
       ///Injected our DbContext into the constructor. 
       ///</summary>
        private readonly H_Plus_SportsContext _context;
        public CustomersController(H_Plus_SportsContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Method correlated to GetCusotmer method, check to see if customer exists or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(c => c.CustomerId == id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Customer>))]
        public IActionResult GetCustomer()
        {
            var results = new ObjectResult(_context.Customer)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            Request.HttpContext.Response.Headers.Add("X-Total-Count", _context.Customer.Count().ToString());

            return results;
        }

        [HttpGet("{Id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("getCustomer", new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id})")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
          
        }

        [HttpDelete("{id})")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> DeleteCustomer([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

    }
}
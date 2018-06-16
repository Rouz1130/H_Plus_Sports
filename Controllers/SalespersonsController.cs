﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H_Plus_Sports.Interfaces;
using H_Plus_Sports.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSportsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Salespersons")]
    public class SalespersonsController : Controller
    {
        private readonly ISalespersonsRepository _salespeople;
        public SalespersonsController(ISalespersonsRepository salespeople)
        {
            _salespeople = salespeople;
        }

        private async Task<bool> SalespersonExists(int id)
        {
            return await _salespeople.Exists(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Salesperson>))]
        public IActionResult GetSalesperson()
        {
            return new OkObjectResult(_salespeople.GetAll());
        }

        [HttpGet("{id}")]
        [Produces(typeof(Salesperson))]
        public async Task<IActionResult> GetSalesperson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesperson = await _salespeople.Find(id);

            if (salesperson == null)
            {
                return NotFound();
            }

            return Ok(salesperson);
        }

        [HttpPost]
        [Produces(typeof(Salesperson))]
        public async Task<IActionResult> PostSalesperson([FromBody] Salesperson salesperson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _salespeople.Add(salesperson);
            }
            catch(DbUpdateException)
            {
                if (!await SalespersonExists(salesperson.SalespersonId))
                {
                    return NotFound();

                }
                else
                {
                    return BadRequest();
                }
            }

            return CreatedAtAction("GetSalesperson", new { id = salesperson.SalespersonId }, salesperson);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Salesperson))]
        public async Task<IActionResult> PutSalesperson([FromRoute] int id, [FromBody] Salesperson salesperson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != salesperson.SalespersonId)
            {
                return BadRequest();
            }

            try
            {
                await _salespeople.Update(salesperson);
                return Ok(salesperson);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SalespersonExists(id))
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
        [Produces(typeof(Salesperson))]
        public async Task<IActionResult> DeleteSalesperson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await SalespersonExists(id))
            {
                return NotFound();
            }

            await _salespeople.Remove(id);

            return Ok();
        }
    }
}
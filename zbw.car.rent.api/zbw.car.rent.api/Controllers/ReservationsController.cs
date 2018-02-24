﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zbw.car.rent.api.Model;
using zbw.car.rent.api.Provider;

namespace zbw.car.rent.api.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IDataProvider<Reservation> _reservationsDataProvider;

        public ReservationsController(IDataProvider<Reservation> reservationsDataProvider)
        {
            _reservationsDataProvider = reservationsDataProvider;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<Reservation>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var objs = await _reservationsDataProvider.GetAllAsync();
                return Ok(objs);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name = "GetReservation")]
        [Produces(typeof(Reservation))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var obj = await _reservationsDataProvider.GetAsync(id);
                if (obj == null)
                    return NotFound(id);

                return Ok(obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPost]
        [Produces(typeof(Reservation))]
        public async Task<IActionResult> Create([FromBody]Reservation reservation)
        {
            if (reservation == null)
                return BadRequest($"{nameof(reservation)} must not be null!");

            try
            {
                var obj = await _reservationsDataProvider.AddAsync(reservation);
                return CreatedAtRoute("GetReservation", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]Reservation reservation)
        {
            try
            {
                var exists = await _reservationsDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _reservationsDataProvider.UpdateAsync(id, reservation);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var exists = await _reservationsDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _reservationsDataProvider.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
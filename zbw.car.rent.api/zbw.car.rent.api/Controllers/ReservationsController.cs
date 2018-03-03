using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zbw.car.rent.api.Model;
using zbw.car.rent.api.Repositories;

namespace zbw.car.rent.api.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IRepository<Reservation> _reservationsRepository;

        public ReservationsController(IRepository<Reservation> reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<Reservation>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var objs = await _reservationsRepository.GetAllAsync();
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
                var obj = await _reservationsRepository.GetAsync(id);
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
                var obj = await _reservationsRepository.AddAsync(reservation);
                return CreatedAtRoute("GetReservation", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]Reservation reservation)
        {
            try
            {
                var exists = await _reservationsRepository.GetAsync(reservation.Id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {reservation.Id}");

                await _reservationsRepository.UpdateAsync(reservation);
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
                var exists = await _reservationsRepository.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _reservationsRepository.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

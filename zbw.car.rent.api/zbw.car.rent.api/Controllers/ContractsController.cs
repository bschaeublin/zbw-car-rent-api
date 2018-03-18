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
    public class ContractsController : Controller
    {
        private readonly IRepository<RentalContract> _contractsRepository;
        private readonly IRepository<Reservation> _reservationsRepository;

        public ContractsController(IRepository<RentalContract> contractsRepository, IRepository<Reservation> reservationsRepository)
        {
            _contractsRepository = contractsRepository;
            _reservationsRepository = reservationsRepository;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<RentalContract>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var objs = await _contractsRepository.GetAllAsync();
                return Ok(objs);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name = "GetContract")]
        [Produces(typeof(RentalContract))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var obj = await _contractsRepository.GetAsync(id);
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
        [Produces(typeof(RentalContract))]
        public async Task<IActionResult> Create([FromBody]RentalContract contract)
        {
            if (contract == null)
                return BadRequest($"{nameof(contract)} must not be null!");

            try
            {
                if (contract.ReservationId != 0)
                {
                    var reservation = await _reservationsRepository.GetAsync(contract.ReservationId);
                    reservation.State = ReservationState.Contracted;
                    await _reservationsRepository.UpdateAsync(reservation);
                }

                var obj = await _contractsRepository.AddAsync(contract);
                return CreatedAtRoute("GetContract", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]RentalContract contract)
        {
            try
            {
                var exists = await _contractsRepository.GetAsync(contract.Id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {contract.Id}");

                await _contractsRepository.UpdateAsync(contract);
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
                var contract = await _contractsRepository.GetAsync(id);
                if (contract == null)
                    return NotFound($"No Object found with ID {id}");

                if (contract.ReservationId != 0)
                {
                    var reservation = await _reservationsRepository.GetAsync(contract.ReservationId);
                    reservation.State = ReservationState.Reserved;
                    await _reservationsRepository.UpdateAsync(reservation);
                }

                await _contractsRepository.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

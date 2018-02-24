using System;
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
    public class ContractsController : Controller
    {
        private readonly IDataProvider<RentalContract> _contractsDataProvider;
        private readonly IDataProvider<Reservation> _reservationsDataProvider;

        public ContractsController(IDataProvider<RentalContract> contractsDataProvider, IDataProvider<Reservation> reservationsDataProvider)
        {
            _contractsDataProvider = contractsDataProvider;
            _reservationsDataProvider = reservationsDataProvider;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<RentalContract>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var objs = await _contractsDataProvider.GetAllAsync();
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
                var obj = await _contractsDataProvider.GetAsync(id);
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
                    var reservation = await _reservationsDataProvider.GetAsync(contract.ReservationId);
                    reservation.State = ReservationState.Contracted;
                    await _reservationsDataProvider.UpdateAsync(contract.ReservationId, reservation);
                }

                var obj = await _contractsDataProvider.AddAsync(contract);
                return CreatedAtRoute("GetContract", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]RentalContract reservation)
        {
            try
            {
                var exists = await _contractsDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _contractsDataProvider.UpdateAsync(id, reservation);
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
                var exists = await _contractsDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _contractsDataProvider.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

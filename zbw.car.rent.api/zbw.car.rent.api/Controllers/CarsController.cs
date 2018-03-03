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
    public class CarsController : Controller
    {
        private readonly IRepository<Car> _carRepository;

        public CarsController(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<Car>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var objs = await _carRepository.GetAllAsync();
                return Ok(objs);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name = "GetCar")]
        [Produces(typeof(Car))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var obj = await _carRepository.GetAsync(id);
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
        [Produces(typeof(Car))]
        public async Task<IActionResult> Create([FromBody]Car car)
        {
            if (car == null)
                return BadRequest($"{nameof(car)} must not be null!");

            try
            {
                var obj = await _carRepository.AddAsync(car);
                return CreatedAtRoute("GetCar", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]Car car)
        {
            try
            {
                var exists = await _carRepository.GetAsync(car.Id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {car.Id}");

                await _carRepository.UpdateAsync(car);
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
                var exists = await _carRepository.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _carRepository.RemoveAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

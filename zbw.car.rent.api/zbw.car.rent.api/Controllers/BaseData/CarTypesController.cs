using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zbw.car.rent.api.Model;
using zbw.car.rent.api.Repositories;

namespace zbw.car.rent.api.Controllers.BaseData
{
    [Route("api/basedata/[controller]")]
    public class CarTypesController : Controller
    {
        private readonly IRepository<CarType> _typeRepository;

        public CarTypesController(IRepository<CarType> typeRepository)
        {
            _typeRepository = typeRepository;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<CarType>))]
        public async Task<IActionResult> GetAllTypes()
        {
            try
            {
                var objs = await _typeRepository.GetAllAsync();
                return Ok(objs);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name = "GetCarType")]
        [Produces(typeof(CarType))]
        public async Task<IActionResult> GetType(int id)
        {
            try
            {
                var obj = await _typeRepository.GetAsync(id);
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
        [Produces(typeof(CarType))]
        public async Task<IActionResult> CreateType([FromBody]CarType carType)
        {
            if (carType == null)
                return BadRequest($"{nameof(carType)} must not be null!");

            try
            {
                var obj = await _typeRepository.AddAsync(carType);
                return CreatedAtRoute("GetCarType", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateType([FromBody]CarType carType)
        {
            try
            {
                var exists = await _typeRepository.GetAsync(carType.Id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {carType.Id}");

                await _typeRepository.UpdateAsync(carType);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            try
            {
                var exists = await _typeRepository.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _typeRepository.RemoveAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

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
    public class CarClassesController : Controller
    {
        private readonly IRepository<CarClass> _classRepository;

        public CarClassesController(IRepository<CarClass> classRepository)
        {
            _classRepository = classRepository;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<CarClass>))]
        public async Task<IActionResult> GetAllClasses()
        {
            try
            {
                var objs = await _classRepository.GetAllAsync();
                return Ok(objs);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name = "GetCarClass")]
        [Produces(typeof(CarClass))]
        public async Task<IActionResult> GetClass(int id)
        {
            try
            {
                var obj = await _classRepository.GetAsync(id);
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
        [Produces(typeof(CarClass))]
        public async Task<IActionResult> CreateClass([FromBody]CarClass carClass)
        {
            if (carClass == null)
                return BadRequest($"{nameof(carClass)} must not be null!");

            try
            {
                var obj = await _classRepository.AddAsync(carClass);
                return CreatedAtRoute("GetCarClass", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass([FromBody]CarClass carClass)
        {
            try
            {
                var exists = await _classRepository.GetAsync(carClass.Id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {carClass.Id}");

                await _classRepository.UpdateAsync(carClass);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            try
            {
                var exists = await _classRepository.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _classRepository.RemoveAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

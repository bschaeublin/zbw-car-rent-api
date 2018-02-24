using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zbw.car.rent.api.Model;
using zbw.car.rent.api.Provider;

namespace zbw.car.rent.api.Controllers.Administration
{
    [Route("api/basedata/[controller]")]
    public class CarClassesController : Controller
    {
        private readonly IDataProvider<CarClass> _classDataProvider;

        public CarClassesController(IDataProvider<CarClass> classDataProvider)
        {
            _classDataProvider = classDataProvider;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<CarClass>))]
        public async Task<IActionResult> GetAllClasses()
        {
            try
            {
                var objs = await _classDataProvider.GetAllAsync();
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
                var obj = await _classDataProvider.GetAsync(id);
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
                var obj = await _classDataProvider.AddAsync(carClass);
                return CreatedAtRoute("GetCarClass", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(int id, [FromBody]CarClass carClass)
        {
            try
            {
                var exists = await _classDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _classDataProvider.UpdateAsync(id, carClass);
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
                var exists = await _classDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _classDataProvider.RemoveAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

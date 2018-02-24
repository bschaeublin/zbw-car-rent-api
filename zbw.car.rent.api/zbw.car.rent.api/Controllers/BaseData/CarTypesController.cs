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
    public class CarTypesController : Controller
    {
        private readonly IDataProvider<CarType> _typeDataProvider;

        public CarTypesController(IDataProvider<CarType> typeDataProvider)
        {
            _typeDataProvider = typeDataProvider;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<CarType>))]
        public async Task<IActionResult> GetAllTypes()
        {
            try
            {
                var objs = await _typeDataProvider.GetAllAsync();
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
                var obj = await _typeDataProvider.GetAsync(id);
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
                var obj = await _typeDataProvider.AddAsync(carType);
                return CreatedAtRoute("GetCarType", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateType(int id, [FromBody]CarType carType)
        {
            try
            {
                var exists = await _typeDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _typeDataProvider.UpdateAsync(id, carType);
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
                var exists = await _typeDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _typeDataProvider.RemoveAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

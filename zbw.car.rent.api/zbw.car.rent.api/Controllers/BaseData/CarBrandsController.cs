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
    public class CarBrandsController : Controller
    {
        private readonly IDataProvider<CarBrand> _brandDataProvider;

        public CarBrandsController(IDataProvider<CarBrand> brandDataProvider)
        {
            _brandDataProvider = brandDataProvider;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<CarBrand>))]
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var objs = await _brandDataProvider.GetAllAsync();
                return Ok(objs);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name = "GetCarBrand")]
        [Produces(typeof(Car))]
        public async Task<IActionResult> GetBrand(int id)
        {
            try
            {
                var obj = await _brandDataProvider.GetAsync(id);
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
        [Produces(typeof(CarBrand))]
        public async Task<IActionResult> CreateBrand([FromBody]CarBrand carBrand)
        {
            if (carBrand == null)
                return BadRequest($"{nameof(carBrand)} must not be null!");

            try
            {
                var obj = await _brandDataProvider.AddAsync(carBrand);
                return CreatedAtRoute("GetCar", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, [FromBody]CarBrand carBrand)
        {
            try
            {
                var exists = await _brandDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _brandDataProvider.UpdateAsync(id, carBrand);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                var exists = await _brandDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _brandDataProvider.RemoveAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

    
    }
}

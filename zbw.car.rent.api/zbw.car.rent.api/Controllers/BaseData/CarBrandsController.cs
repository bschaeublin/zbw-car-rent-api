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
    public class CarBrandsController : Controller
    {
        private readonly IRepository<CarBrand> _brandRepository;

        public CarBrandsController(IRepository<CarBrand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<CarBrand>))]
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var objs = await _brandRepository.GetAllAsync();
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
                var obj = await _brandRepository.GetAsync(id);
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
                var obj = await _brandRepository.AddAsync(carBrand);
                return CreatedAtRoute("GetCar", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand([FromBody]CarBrand carBrand)
        {
            try
            {
                var exists = await _brandRepository.GetAsync(carBrand.Id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {carBrand.Id}");

                await _brandRepository.UpdateAsync(carBrand);
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
                var exists = await _brandRepository.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _brandRepository.RemoveAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

    
    }
}

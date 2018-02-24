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
    public class CustomersController : Controller
    {
        private readonly IDataProvider<Customer> _customerDataProvider;

        public CustomersController(IDataProvider<Customer> customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<Customer>))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var objs = await _customerDataProvider.GetAllAsync();
                return Ok(objs);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var obj = await _customerDataProvider.GetAsync(id);
                if (obj == null)
                    return NotFound(id);

                return Ok(obj);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPost]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> Create([FromBody]Customer customer)
        {
            if (customer == null)
                return BadRequest($"{nameof(customer)} must not be null!");
            
            try
            {
                var obj = await _customerDataProvider.AddAsync(customer);
                return CreatedAtRoute("GetCustomer", new { id = obj.Id }, obj);
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]Customer customer)
        {
            try
            {
                var exists = await _customerDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _customerDataProvider.UpdateAsync(id, customer);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var exists = await _customerDataProvider.GetAsync(id) != null;
                if (!exists)
                    return NotFound($"No Object found with ID {id}");

                await _customerDataProvider.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}

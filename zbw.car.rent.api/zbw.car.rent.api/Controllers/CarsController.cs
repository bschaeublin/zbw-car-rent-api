using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zbw.car.rent.api.Model;
using zbw.car.rent.api.Provider;

namespace zbw.car.rent.api.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : Controller
    {
        private readonly IDataProvider<Car> _carDataProvider;

        public CarsController(IDataProvider<Car> carDataProvider)
        {
            _carDataProvider = carDataProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> Get()
        {
            return await _carDataProvider.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Car> Get(int id)
        {
            return await _carDataProvider.GetAsync(id);
        }

        [HttpPost]
        public async Task<Car> Post([FromBody]Car car)
        {
            return await _carDataProvider.AddAsync(car);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Car car)
        {
           await _carDataProvider.UpdateAsync(id, car);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _carDataProvider.RemoveAsync(id);
        }
    }
}

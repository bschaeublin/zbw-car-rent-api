using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using zbw.car.rent.api.Model;

namespace zbw.car.rent.api.Repositories.Database
{
    public class DatabaseRepository<T> : IRepository<T> where T: class, IDataObj
    {
        private readonly CarRentDbContext _dbContext;
        public DatabaseRepository(CarRentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<T>> GetAllAsync()
        {
            return _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public Task<T> GetAsync(int id)
        {
            return _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T obj)
        {
            await _dbContext.Set<T>().AddAsync(obj);
            await _dbContext.SaveChangesAsync();

            return obj;
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await GetAsync(id);
            _dbContext.Set<T>().Remove(obj);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T obj)
        {
            _dbContext.Set<T>().Update(obj);
            await _dbContext.SaveChangesAsync();
        }
    }
}

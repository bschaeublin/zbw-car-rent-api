﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace zbw.car.rent.api.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T obj);
        Task RemoveAsync(int id);
        Task UpdateAsync(T obj);
    }
}

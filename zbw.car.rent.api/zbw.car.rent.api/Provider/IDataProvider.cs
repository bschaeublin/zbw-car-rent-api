using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zbw.car.rent.api.Model;

namespace zbw.car.rent.api.Provider
{
    public interface IDataProvider<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T obj);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, T obj);
    }
}

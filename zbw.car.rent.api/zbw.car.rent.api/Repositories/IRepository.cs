using System.Collections.Generic;
using System.Threading.Tasks;

namespace zbw.car.rent.api.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T obj);
        Task RemoveAsync(int id);
        Task UpdateAsync(int id, T obj);
    }
}

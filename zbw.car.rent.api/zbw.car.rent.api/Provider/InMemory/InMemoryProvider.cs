using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zbw.car.rent.api.Model;

namespace zbw.car.rent.api.Provider.InMemory
{
    public class InMemoryProvider<T>: IDataProvider<T> where T : IDataObj
    {
        private IEnumerable<T> _objs = new List<T>();

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.Run(() => _objs);
        }

        public Task<T> GetAsync(int id)
        {
            return Task.Run(() => _objs.FirstOrDefault(c => c.Id == id));
        }

        public Task<T> AddAsync(T obj)
        {
            return Task.Run(() =>
            {
                obj.Id = _objs.Max(c => c.Id) + 1;
                ((List<T>) _objs).Add(obj);
                return obj;
            });
        }

        public Task RemoveAsync(int id)
        {
            return Task.Run(() =>
            {
                ((List<T>)_objs).RemoveAll(o => o.Id == id);
            });
        }

        public async Task UpdateAsync(int id, T obj)
        {
            var old = await GetAsync(id);

            await Task.Run(() =>
            {
                var index = ((List<T>) _objs).IndexOf(old);
                ((List<T>)_objs)[index] = obj;
            });
        }
    }
}

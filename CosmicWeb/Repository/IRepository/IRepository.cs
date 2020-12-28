using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(string url, int Id);
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<bool> CreateAsync(string url,T objtoCreate);
        Task<bool> UpdateAsync(string url, T objtoUpdate);
        Task<bool> DeleteAysnc(string url, T objtoCreate, int Id);
    }
}

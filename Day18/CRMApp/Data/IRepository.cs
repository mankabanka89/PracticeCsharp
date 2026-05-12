using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMApp.Data
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
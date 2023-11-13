using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task SaveChangesAsync();

        Task<T?> GetByIdAsync(int id);

        Task<List<T>?> GetAllAsync();

        Task CreateAsync(T entity);

        void UpdateAsync(T entity);

        void Delete(T entity);
    }
}

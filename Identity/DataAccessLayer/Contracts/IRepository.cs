using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace DataAccessLayer.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task SaveChangesAsync();

        Task<T?> GetByIdAsync(int id);

        Task<List<T>?> GetAllAsync(int offset,int limit);

        Task<EntityEntry<T>> CreateAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}

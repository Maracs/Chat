using DataAccessLayer.Contracts;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace DataAccessLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DatabaseContext _databaseContext;

        public Repository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<EntityEntry<T>> CreateAsync(T entity)
        {
           return await _databaseContext.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _databaseContext.Remove(entity);
        }

        public async Task<List<T>?> GetAllAsync(int offset, int limit)
        {
            var entities =await  _databaseContext.Set<T>()
                .Skip(offset)
                .Take(limit)
                .AsNoTracking()
                .ToListAsync();

            return entities;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _databaseContext.FindAsync<T>(id);
          
            return entity;      
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _databaseContext.Update(entity);
        }
    }
}

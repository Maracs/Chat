using DataAccessLayer.Contracts;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<T>?> GetAllAsync()
        {
            var entities =await  _databaseContext.Set<T>().ToListAsync();
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

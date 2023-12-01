using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> GetByIdAsync(int id);

        Task<List<Chat>> GetAllAsync(int userId,int offset, int limit);

        Task CreateAsync(Chat chat);

        void Delete(int id);

        void Update(Chat chat);

        Task SaveChangesAsync();
    }
}

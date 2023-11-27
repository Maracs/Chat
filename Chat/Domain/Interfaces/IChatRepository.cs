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

        Task<List<Chat>> GetAllAsync(int id);

        Task CreateAsync(Chat chat);

        Task DeleteAsync(int id);

        Task<Chat> UpdateAsync(int id, Chat chat);
    }
}

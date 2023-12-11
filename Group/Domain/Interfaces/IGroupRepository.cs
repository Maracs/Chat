using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetByIdAsync(int id);

        Task<List<Group>> GetAllAsync(int userId, int offset, int limit);

        Task CreateAsync(Group group);

        void Delete(int id);

        void Update(Group group);

        Task SaveChangesAsync();
    }
}

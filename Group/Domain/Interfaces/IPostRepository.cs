using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPostRepository
    {
     
        Task<Post> GetByIdAsync(int chatId, int id);

        Task<List<Post>> GetAllAsync(int userId, int chatId, int offset, int limit);

        Task SendAsync(Post post,List<string> photos);

        void Delete(int groupId, int id);

        void Update(Post post);

        Task SaveChangesAsync();
    }
}

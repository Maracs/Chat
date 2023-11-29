using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserChatRepository
    {
        void Delete(ChatUser userChatD);
        
        Task CreateAsync(ChatUser userChatD);

        Task SaveChangesAsync();
    }
}

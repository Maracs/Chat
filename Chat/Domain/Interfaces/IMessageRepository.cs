using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task ChangeMessageStatusAsync(int chatid, int id, string status);

        Task<List<Chat>> GetByIdAsync(int chatid);

        Task SendAsync(ChatMessage chatMessage, Message message);

        void DeleteAsync(int chatid, int id);

        void UpdateAsync(Message message);

        Task SaveChangesAsync();
    }
}

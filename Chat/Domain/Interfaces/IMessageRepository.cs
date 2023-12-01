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

        Task<ChatMessage> GetByIdAsync(int chatid, int id);

        Task<List<ChatMessage>> GetAllAsync(int userId, int chatid, int offset, int limit);

        Task SendAsync(ChatMessage chatMessage, Message message);

        Task<int> GetStatusAsync(string status);

        void Delete(int chatid, int id);

        void Update(Message message);

        Task SaveChangesAsync();
    }
}

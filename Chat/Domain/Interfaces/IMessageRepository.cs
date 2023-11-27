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
        Task<Chat> ChangeMessageStatusAsync(int chatid, int id, string status);

        Task<List<Chat>> GetByIdAsync(int chatid);

        Task SendAsync(Chat chat);

        Task ResendAsync(Chat chat);

        Task DeleteAsync(int chatid, int id);

        Task<Message> UpdateAsync(int chatid, int id, Message message);
    }
}

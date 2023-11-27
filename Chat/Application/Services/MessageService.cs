using Application.Dtos;
using Application.Ports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MessageService : IMessageService
    {
        public Task<ChatDto> ChangeMessageStatusAsync(int chatid, int id, string status)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int chatid, int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChatDto>> GetByIdAsync(int chatid)
        {
            throw new NotImplementedException();
        }

        public Task ResendAsync(CreateChatDto chatDto)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(CreateChatDto chatDto)
        {
            throw new NotImplementedException();
        }

        public Task<MessageDto> UpdateAsync(int chatid, int id, MessageDto chatDto)
        {
            throw new NotImplementedException();
        }
    }
}

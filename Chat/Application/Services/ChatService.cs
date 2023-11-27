using Application.Dtos;
using Application.Ports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        public Task CreateAsync(CreateChatDto chatDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChatDto>> GetAllAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ChatDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ChatDto> UpdateAsync(int id, CreateChatDto chatDto)
        {
            throw new NotImplementedException();
        }
    }
}

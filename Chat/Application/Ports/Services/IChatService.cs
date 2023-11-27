using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports.Services
{
    public interface IChatService
    {
        Task<ChatDto> GetByIdAsync(int userId, int id);

        Task<List<ChatDto>> GetAllAsync(int userId);

        Task CreateAsync(int userId,CreateChatDto chatDto);

        Task DeleteAsync(int userId,int id);

        Task<ChatDto> UpdateAsync(int userId,int id, CreateChatDto chatDto);
    }
}

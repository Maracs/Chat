using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports.Services
{
    public interface IUserChatService
    {
        Task CreateAsync(int userId, UserChatDto userChatDto);

        Task DeleteAsync(int userId, UserChatDto userChatDto);
    }
}

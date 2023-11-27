using Application.Dtos;
using Application.Ports.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserChatService : IUserChatService
    {
        public Task CreateAsync(UserChatDto userChatDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserChatDto userChatDto)
        {
            throw new NotImplementedException();
        }
    }
}

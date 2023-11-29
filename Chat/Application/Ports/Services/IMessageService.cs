using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports.Services
{
    public interface IMessageService
    {
        Task<ChatDto> ChangeMessageStatusAsync(int chatid,int id,string status);

        Task<List<ChatDto>> GetByIdAsync(int chatid);

        Task SendAsync(MessageDto messageDto);

        Task DeleteAsync(int chatid, int id);

        Task UpdateAsync(int chatid, int id, MessageDto messageDto);
    }
}

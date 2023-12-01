﻿using Application.Dtos;

namespace Application.Ports.Services
{
    public interface IMessageService
    {
        Task ChangeMessageStatusAsync(int userId, int chatid, int id, string status);

        Task<List<MessageDto>> GetAllAsync(int userId, int chatid, int offset, int limit);

        Task SendAsync(int userId, MessageDto messageDto);

        Task DeleteAsync(int userId, int chatid, int id);

        Task UpdateAsync(int userId, int chatid, int id, string content);

    }    
}
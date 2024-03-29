﻿using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserChatRepository
    {
        void Delete(ChatUser userChat);

        Task CreateAsync(ChatUser userChat);

        void DeleteUserFromChats(int id);
        Task SaveChangesAsync();
    }
}

using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {

        private readonly DatabaseContext _db;

        public ChatRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Chat chat)
        {
           await _db.Chats.AddAsync(chat);
        }

        public  void Delete(int id)
        {
            var user = _db.Chats.Where(chat => chat.Id == id).Single();
           _db.Chats.Remove(user);
        }

        public async Task<List<Chat>> GetAllAsync(int userId,int offset,int limit)
        {
            
            return await _db.Chats
                .Include(chat=>chat.Users)
                .Include(chat=>chat.Messages)
                .AsNoTracking()
                .Where(chat=>chat.Users.Where(users=>users.UserId==userId).FirstOrDefault()!=null)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
        }

        

        public async Task<Chat> GetByIdAsync(int id)
        {
            
            return await _db.Chats.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
           await _db.SaveChangesAsync();
        }

        public void Update(Chat chat)
        {
             _db.Chats.Update(chat);
        }
    }
}

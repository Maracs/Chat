using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteAsync(int id)
        {
            var user = _db.Chats.Where(chat => chat.Id == id).Single();
           _db.Chats.Remove(user);
        }

        public async Task<List<Chat>> GetAllAsync(int offset,int limit)
        {
            
            return await _db.Chats
                .AsNoTracking()
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

        public void UpdateAsync(Chat chat)
        {
             _db.Chats.Update(chat);
        }
    }
}

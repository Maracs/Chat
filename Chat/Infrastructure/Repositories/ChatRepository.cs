using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {

        private readonly DatabaseContext _databaseContext;

        public ChatRepository(DatabaseContext db)
        {
            _databaseContext = db;
        }

        public async Task CreateAsync(Chat chat)
        {
           await _databaseContext.Chats.AddAsync(chat);
        }

        public  void Delete(int id)
        {
            var chat = new Chat() { Id = id };
           _databaseContext.Chats.Remove(chat);
        }

        public async Task<List<Chat>> GetAllAsync(int userId,int offset,int limit)
        {

            var chats = await _databaseContext.Chats
                .Include(chat => chat.Users)
                .Where(chat => chat.Users.Where(users => users.UserId == userId).FirstOrDefault() != null || chat.CreatorId == userId)
                .Skip(offset)
                .Take(limit)
                .Include(chat => chat.Messages)
                    .ThenInclude(message=>message.Message)
                .Include(chat => chat.Messages)
                    .ThenInclude(message => message.MessageStatus)
                .AsNoTracking().ToListAsync();

            return chats;
        }

        

        public async Task<Chat> GetByIdAsync(int id)
        {
            
            return await _databaseContext.Chats
                .Include(chat=>chat.Users)
                .Include(chat=>chat.Messages)
                    .ThenInclude(message => message.Message)
                .Include(chat => chat.Messages)
                    .ThenInclude(message => message.MessageStatus)
                .AsNoTracking()
                .Where(chat=>chat.Id==id)
                .SingleAsync();
        }

        public async Task SaveChangesAsync()
        {
           await _databaseContext.SaveChangesAsync();
        }

        public void Update(Chat chat)
        {
             _databaseContext.Chats.Update(chat);
        }
    }
}

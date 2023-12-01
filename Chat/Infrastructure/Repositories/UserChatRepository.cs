using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class UserChatRepository : IUserChatRepository
    {
        private readonly DatabaseContext _db;

        public UserChatRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(ChatUser userChat)
        {
          await _db.ChatUsers.AddAsync(userChat);
        }

        public void Delete(ChatUser userChat)
        {
            _db.ChatUsers.Remove(userChat);
        }

        public async Task SaveChangesAsync()
        {
           await _db.SaveChangesAsync();
        }
    }
}

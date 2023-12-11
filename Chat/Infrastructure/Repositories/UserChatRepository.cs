using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class UserChatRepository : IUserChatRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserChatRepository(DatabaseContext db)
        {
            _databaseContext = db;
        }

        public async Task CreateAsync(ChatUser userChat)
        {
            await _databaseContext.ChatUsers.AddAsync(userChat);
        }

        public void Delete(ChatUser userChat)
        {
            _databaseContext.ChatUsers.Remove(userChat);
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}

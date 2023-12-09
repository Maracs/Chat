using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {

        private readonly DatabaseContext _databaseContext;

        public MessageRepository(DatabaseContext db)
        {
            _databaseContext = db;
        }

        public async Task ChangeMessageStatusAsync(int chatId, int id, string status)
        {
            var message = await _databaseContext.ChatMessages.Where(message=>message.ChatId==chatId && message.MessageId == id).SingleAsync();
            message.MessageStatusId = await GetStatusAsync(status);
            _databaseContext.Update(message);

        }

        public async Task<int> GetStatusAsync(string status)
        {
            return (await _databaseContext.MessageStatuses.Where(s => s.Status == status).SingleAsync()).Id;
        }

        public void Delete(int chatid, int id)
        {
           var chatMessage = _databaseContext.ChatMessages.Where(message=> message.MessageId == id).Single();
           var message = _databaseContext.Messages.Where(message => message.Id == id).Single(); 
            _databaseContext.Remove(chatMessage);
            _databaseContext.Remove(message);
            
        }

        public Task<List<ChatMessage>> GetAllAsync(int userId, int chatid, int offset, int limit)
        {
            return _databaseContext.ChatMessages.Include(chat => chat.Message)
                .Include(chat=>chat.MessageStatus)
                .Where(message => message.ChatId == chatid && message.UserId == userId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync() ;
        }

        public Task<ChatMessage> GetByIdAsync(int chatid,int id)
        {
            return _databaseContext.ChatMessages.Include(chat=>chat.Message).Where(message=> message.ChatId == chatid && message.MessageId==id).SingleAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public async Task SendAsync(ChatMessage chatMessage,Message message)
        {
           var mes = await _databaseContext.Messages.AddAsync(message);
           await _databaseContext.SaveChangesAsync();
           chatMessage.MessageId = mes.Entity.Id;
           await _databaseContext.ChatMessages.AddAsync(chatMessage);
        }

        public void Update(Message message)
        {
            _databaseContext.Update(message);
        }
    }
}

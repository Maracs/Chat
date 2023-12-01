using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {

        private readonly DatabaseContext _db;

        public MessageRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task ChangeMessageStatusAsync(int chatid, int id, string status)
        {
            var message = await _db.ChatMessages.Where(message=>message.ChatId==chatid && message.MessageId == id).SingleAsync();

            message.MessageStatusId = await GetStatusAsync(status);

            _db.Update(message);

        }

        public async Task<int> GetStatusAsync(string status)
        {
            return (await _db.MessageStatuses.Where(s => s.Status == status).SingleAsync()).Id;
        }

        public void Delete(int chatid, int id)
        {
           var chatMessage = _db.ChatMessages.Where(message=> message.MessageId == id).Single();
           var message = _db.Messages.Where(message => message.Id == id).Single(); 
            _db.Remove(chatMessage);
            _db.Remove(message);
            
        }

        public Task<List<ChatMessage>> GetAllAsync(int userId, int chatid, int offset, int limit)
        {
            return _db.ChatMessages.Include(chat => chat.Message)
                .Include(chat=>chat.MessageStatus)
                .Where(message => message.ChatId == chatid && message.UserId == userId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync() ;
        }

        public Task<ChatMessage> GetByIdAsync(int chatid,int id)
        {
            return _db.ChatMessages.Include(chat=>chat.Message).Where(message=> message.ChatId == chatid && message.MessageId==id).SingleAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task SendAsync(ChatMessage chatMessage,Message message)
        {
           var mes = await _db.Messages.AddAsync(message);
           await _db.SaveChangesAsync();
           chatMessage.MessageId = mes.Entity.Id;
           await _db.ChatMessages.AddAsync(chatMessage);
        }


        public void Update(Message message)
        {
            _db.Update(message);
        }
    }
}

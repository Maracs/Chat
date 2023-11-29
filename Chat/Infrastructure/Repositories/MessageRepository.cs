using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var message = await _db.ChatMessages.Where(message=>message.ChatId==chatid && message.UserId == id).SingleAsync();

            var statusId = await _db.MessageStatuses.Where(s => s.Status == status).SingleAsync();

            message.MessageStatusId = statusId.Id;

            _db.Update(message);

        }

        public async void DeleteAsync(int chatid, int id)
        {
           var chatMessage = await _db.ChatMessages.Where(message=>message.ChatId==chatid && message.UserId == id).SingleAsync();
           var message = chatMessage.Message;
            _db.Remove(message);
            _db.Remove(chatMessage);
        }

        public Task<List<Chat>> GetByIdAsync(int chatid)
        {
            return _db.Chats.Include(chat=>chat.Messages).Where(chat=>chat.Id == chatid).ToListAsync();
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


        public void UpdateAsync(Message message)
        {
            _db.Update(message);
        }
    }
}

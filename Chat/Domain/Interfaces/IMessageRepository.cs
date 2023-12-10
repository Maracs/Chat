using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task ChangeMessageStatusAsync(int chatId, int id, string status);

        Task<ChatMessage> GetByIdAsync(int chatId, int id);

        Task<List<ChatMessage>> GetAllAsync(int userId, int chatId, int offset, int limit);

        Task SendAsync(ChatMessage chatMessage, Message message);

        Task<int> GetStatusAsync(string status);

        void Delete(int chatId, int id);

        void Update(Message message);

        Task SaveChangesAsync();
    }
}

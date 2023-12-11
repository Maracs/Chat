using Application.Dtos;

namespace Application.Ports.Services
{
    public interface IMessageService
    {
        Task ChangeMessageStatusAsync(int userId, int chatid, int id, string status, CancellationToken token);

        Task<List<MessageDto>> GetAllAsync(int userId, int chatid, int offset, int limit, CancellationToken token);

        Task SendAsync(int userId, MessageDto messageDto, CancellationToken token);

        Task DeleteAsync(int userId, int chatid, int id, CancellationToken token);

        Task UpdateAsync(int userId, int chatid, int id, string content, CancellationToken token);
    }
}

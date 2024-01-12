using Application.Dtos;

namespace Application.Ports.Services
{
    public interface IChatService
    {
        Task<ChatWithUserNicknameDto> GetByIdAsync(int userId, int id, CancellationToken token);

        Task<List<ChatDto>> GetAllAsync(int userId, int offset, int limit, CancellationToken token);

        Task CreateAsync(int userId, CreateChatDto chatDto, CancellationToken token);

        Task DeleteAsync(int userId, int id, CancellationToken token);

        Task UpdateAsync(int userId, int id, CreateChatDto chatDto, CancellationToken token);
    }
}

using Application.Dtos;


namespace Application.Ports.Services
{
    public interface IChatService
    {
        Task<ChatDto> GetByIdAsync(int userId, int id);

        Task<List<ChatDto>> GetAllAsync(int userId,int offset,int limit);

        Task CreateAsync(int userId,CreateChatDto chatDto);

        Task DeleteAsync(int userId,int id);

        Task UpdateAsync(int userId,int id, CreateChatDto chatDto);
    }
}

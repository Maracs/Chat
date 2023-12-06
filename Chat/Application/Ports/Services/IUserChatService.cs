using Application.Dtos;

namespace Application.Ports.Services
{
    public interface IUserChatService
    {
        Task CreateAsync(int userId, UserChatDto userChatDto,CancellationToken token);

        Task DeleteAsync(int userId, UserChatDto userChatDto,CancellationToken token);
    }
}

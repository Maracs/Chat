using Application.Dtos;


namespace Application.Ports.Services
{
    public interface IUserChatService
    {
        Task CreateAsync(int userId, UserChatDto userChatDto);

        Task DeleteAsync(int userId, UserChatDto userChatDto);
    }
}

using Domain.Entities;


namespace Domain.Interfaces
{
    public interface IUserChatRepository
    {
        void Delete(ChatUser userChatD);
        
        Task CreateAsync(ChatUser userChatD);

        Task SaveChangesAsync();
    }
}

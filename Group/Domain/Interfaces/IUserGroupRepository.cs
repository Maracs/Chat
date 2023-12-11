using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserGroupRepository
    {
        void Delete(GroupUser groupUser);

        Task AddAsync(GroupUser groupUser);

        Task RequestAsync(JoinRequest joinRequest);

        Task SaveChangesAsync();
    }
}

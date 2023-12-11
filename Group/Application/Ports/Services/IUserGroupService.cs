using Application.Dtos;

namespace Application.Ports.Services
{
    public interface IUserGroupService
    {
        Task AddAsync(int userId, UserGroupDto userGroupDto, CancellationToken token);

        Task DeleteAsync(int userId, UserGroupDto userGroupDto, CancellationToken token);

        Task RequestAsync(int userId, UserGroupDto userGroupDto, CancellationToken token);
    }
}

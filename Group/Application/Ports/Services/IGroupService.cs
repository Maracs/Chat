using Application.Dtos;

namespace Application.Ports.Services
{
    public interface IGroupService
    {
        Task<GroupDto> GetByIdAsync(int userId, int id, CancellationToken token);

        Task<List<GroupDto>> GetAllAsync(int userId, int offset, int limit, CancellationToken token);

        Task CreateAsync(int userId, CreateGroupDto groupDto, CancellationToken token);

        Task DeleteAsync(int userId, int id, CancellationToken token);

        Task UpdateAsync(int userId, int id, CreateGroupDto groupDto, CancellationToken token);
    }
}

using Application.Dtos;

namespace Application.Ports.Services
{
    public interface IPostService
    {
        Task<List<PostDto>> GetAllAsync(int userId, int groupId, int offset, int limit, CancellationToken token);

        Task SendAsync(int userId, PostDto postDto, CancellationToken token);

        Task DeleteAsync(int userId, int groupId, int id, CancellationToken token);

        Task UpdateAsync(int userId, int groupId, int id, string content, CancellationToken token);
    }
}

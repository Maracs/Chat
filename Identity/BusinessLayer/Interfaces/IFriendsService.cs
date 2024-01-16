using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IFriendsService
    {
        public Task AddFriendAsync(int userId, int fid);

        public Task DeleteFriendAsync(int userId, int fid);

        public Task<List<FullUserInfoDto>> GetFriendsAsync(int id,int offset,int limit);
    }
}

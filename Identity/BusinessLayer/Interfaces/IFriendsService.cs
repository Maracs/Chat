using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IFriendsService
    {
        public Task AddFriendAsync(int userId, int fid);

        public Task DeleteFriendAsync(int userId, int fid);

        public Task<List<FullUserInfoDto>> GetFriendsAsync(int id,int offset,int limit);
    }
}

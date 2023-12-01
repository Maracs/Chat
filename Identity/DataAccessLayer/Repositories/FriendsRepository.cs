using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repositories
{
    public class FriendsRepository:Repository<Friend>
    {
        public FriendsRepository(DatabaseContext databaseContext) 
            : base(databaseContext)
        {

        }

        public Task<bool> IfExists(int userId, int friendId)
        {
            return _databaseContext.Friends.AnyAsync(f => f.UserId == userId && f.UserFriendId == friendId);
        }

        public async Task<List<Friend>> GetFriendsAsync(int id,int offset,int limit)
        {
            return await _databaseContext.Friends.Include(f=>f.UserFriend)
                    .ThenInclude(u=>u.UserInfo)
                .Include(f=>f.UserFriend)
                    .ThenInclude(u=>u.Role)
                .Where(f => f.UserId == id)
                .Skip(offset)
                .Take(limit)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

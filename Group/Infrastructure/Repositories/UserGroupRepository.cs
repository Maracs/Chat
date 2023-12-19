using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserGroupRepository(DatabaseContext db)
        {
            _databaseContext = db;
        }

        public async Task AddAsync(GroupUser groupUser)
        {
            var joinRequest = new JoinRequest() { UserId = groupUser.UserId,GroupId = groupUser.GroupId };
            _databaseContext.JoinRequests.Remove(joinRequest);
            await _databaseContext.GroupUsers.AddAsync(groupUser);
        }

        public void Delete(GroupUser groupUser)
        {
            _databaseContext.GroupUsers.Remove(groupUser);
        }

        public async Task RequestAsync(JoinRequest joinRequest)
        {
            await _databaseContext.JoinRequests.AddAsync(joinRequest);
        }

        public async Task SaveChangesAsync(CancellationToken token)
        {
            await _databaseContext.SaveChangesAsync(token);
        }
    }
}

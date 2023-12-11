using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DatabaseContext _databaseContext;

        public GroupRepository(DatabaseContext db)
        {
            _databaseContext = db;
        }

        public async Task CreateAsync(Group group)
        {
            await _databaseContext.Groups.AddAsync(group);
        }

        public void Delete(int id)
        {
            var group = new Group() { Id = id };
            _databaseContext.Groups.Remove(group);
        }

        public async Task<List<Group>> GetAllAsync(int userId, int offset, int limit)
        {
            var chats = await _databaseContext.Groups
                .Include(group => group.Users)
                .Where(group => group.Users.Where(users => users.UserId == userId).FirstOrDefault() != null || group.CreatorId == userId)
                .Skip(offset)
                .Take(limit)
                .Include(group => group.Posts)
                    .ThenInclude(posts => posts.Photos)
                .AsNoTracking().ToListAsync();

            return chats;
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await _databaseContext.Groups
                .Include(group => group.Users)
                .Include(group => group.Posts)
                    .ThenInclude(posts => posts.Photos)
                .AsNoTracking()
                .Where(group => group.Id == id)
                .SingleAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }

        public void Update(Group group)
        {
            _databaseContext.Groups.Update(group);
        }
    }
}

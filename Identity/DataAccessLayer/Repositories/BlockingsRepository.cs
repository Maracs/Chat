using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BlockingsRepository:Repository<Blocked>
    {

        public BlockingsRepository(DatabaseContext databaseContext) 
            : base(databaseContext)
        {

        }

        public Task<bool> IfExists(int userId,int blockedId)
        {
            return _databaseContext.Blockeds.AnyAsync(b => b.UserId == userId && b.BlockedUserId == blockedId);
        }

        public async Task<List<Blocked>> GetBlockingsAsync(int id)
        {
            return await _databaseContext.Blockeds.Include(f => f.BlockedUser)
                    .ThenInclude(u => u.UserInfo)
                .Where(f => f.UserId == id)
                .ToListAsync();
        }
    }
}

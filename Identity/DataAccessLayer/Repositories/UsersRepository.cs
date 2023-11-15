using DataAccessLayer.Contracts;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UsersRepository : Repository<User>
    {
        public UsersRepository(DatabaseContext databaseContext)
        :base(databaseContext)
        {
            
        }

        public async Task<User> GetByAccountnameAsync(string? accountName)
        {
            var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.AccountName == accountName);
            if (user == null) { throw new NullReferenceException(); }
            return user;
        }

        public async Task<string> GetUserRoleAsync(User user)
        {
            var role = await _databaseContext.Roles.FindAsync(user.RoleId);
            if (role == null) { throw new NullReferenceException(); }
            return role.Name;
        }

        public async Task<bool> ifAccountExistsAsync(string accountName)
        {
            return await _databaseContext.Users.AnyAsync(x => x.AccountName == accountName);
        }

        public async Task CreateUserInfoAsync(UserInfo userInfo)
        {
            await _databaseContext.UserInfos.AddAsync(userInfo);
        }

    }
}

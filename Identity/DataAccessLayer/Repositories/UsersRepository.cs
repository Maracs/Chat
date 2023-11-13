using DataAccessLayer.Contracts;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
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

        public async Task<string> GetUserRoleAsync(User user)
        {
            var role = await _databaseContext.Roles.FindAsync(user.RoleId);
            if (role == null) { throw new NullReferenceException(); }
            return role.Name;
        }


    }
}

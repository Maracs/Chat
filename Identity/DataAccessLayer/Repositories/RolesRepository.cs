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
    public class RolesRepository:Repository<Role>
    {
        public RolesRepository(DatabaseContext databaseContext) 
            : base(databaseContext)
        {

        }

        public async Task<int> GetRoleIdAsync(string roleName)
        {
          var role = await _databaseContext.Roles.FirstOrDefaultAsync(x=>x.Name ==roleName);
          
          return role.Id;
        }

    }
}

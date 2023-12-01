using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;


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
          var role = await _databaseContext.Roles.AsNoTracking().FirstOrDefaultAsync(x=>x.Name ==roleName);
          
          return role.Id;
        }

    }
}

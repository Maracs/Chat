using DataAccessLayer.Data;
using DataAccessLayer.Entities;
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
    }
}

using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class RolesService
    {
        private readonly RolesRepository _rolesRepository;

        public RolesService(RolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }
    }
}

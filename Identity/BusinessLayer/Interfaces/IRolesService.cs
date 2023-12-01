using BusinessLayer.DTOs;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRolesService
    {
        public Task AddRoleAsync(string role);

        public Task<List<Role>> GetRolesAsync(int offset, int limit);

        public Task UpdateRoleAsync(RoleDto roleDto);
    }
}

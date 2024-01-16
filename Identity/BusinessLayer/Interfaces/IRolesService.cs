using BusinessLayer.DTOs;
using DataAccessLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IRolesService
    {
        public Task AddRoleAsync(string role);

        public Task<List<Role>> GetRolesAsync(int offset, int limit);

        public Task UpdateRoleAsync(RoleDto roleDto);
    }
}

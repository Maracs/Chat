using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;


namespace BusinessLayer.Services
{
    public class RolesService:IRolesService
    {
        private readonly RolesRepository _rolesRepository;

        public RolesService(RolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task AddRoleAsync(string role)
        {
            await _rolesRepository.CreateAsync(new Role() { Name = role });
            await _rolesRepository.SaveChangesAsync();
        }

        public async Task<List<Role>> GetRolesAsync(int offset, int limit)
        {
            return await _rolesRepository.GetAllAsync(offset,limit);
        }

        public async Task UpdateRoleAsync(RoleDto roleDto)
        {
            var role = await _rolesRepository.GetByIdAsync(roleDto.Id);
            role.Name = roleDto.Name;
            _rolesRepository.Update(role);
            await _rolesRepository.SaveChangesAsync();
        }
    }
}

using BusinessLayer.DTOs;
using DataAccessLayer.Entities;
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

        public async Task AddRoleAsync(string role)
        {
            await _rolesRepository.CreateAsync(new Role() { Name = role });
            await _rolesRepository.SaveChangesAsync();
        }

        public Task<List<Role>> GetRolesAsync()
        {
            return _rolesRepository.GetAllAsync();
        }

        public async Task UpdateRoleAsync(RoleDto role)
        {
           var databaseRole = await _rolesRepository.GetByIdAsync(role.Id);
            databaseRole.Name = role.Name;
            _rolesRepository.Update(databaseRole);
            await _rolesRepository.SaveChangesAsync();
        }
    }
}

using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;


namespace BusinessLayer.Services
{
    public class RolesService:IRolesService
    {
        private readonly RolesRepository _rolesRepository;
        private readonly ILogger<RolesService> _logger;

        public RolesService(RolesRepository rolesRepository,ILogger<RolesService> logger)
        {
            _rolesRepository = rolesRepository;
            _logger = logger;
        }

        public async Task AddRoleAsync(string role)
        {
            _logger.LogInformation(
              "Trying to add role."
            );

            await _rolesRepository.CreateAsync(new Role() { Name = role });
            await _rolesRepository.SaveChangesAsync();

            _logger.LogInformation(
              "Successfully add role."
            );
        }

        public async Task<List<Role>> GetRolesAsync(int offset, int limit)
        {
            _logger.LogInformation(
              "Trying to get roles."
            );

            var roles = await _rolesRepository.GetAllAsync(offset, limit);

            _logger.LogInformation(
              "Successfully get roles."
            );

            return roles;
        }

        public async Task UpdateRoleAsync(RoleDto roleDto)
        {
            _logger.LogInformation(
              "Trying to update role."
            );

            var role = await _rolesRepository.GetByIdAsync(roleDto.Id);
            role.Name = roleDto.Name;
            _rolesRepository.Update(role);
            await _rolesRepository.SaveChangesAsync();

            _logger.LogInformation(
              "Successfully update role."
            );
        }
    }
}

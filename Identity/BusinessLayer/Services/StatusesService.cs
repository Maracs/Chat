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
    public class StatusesService
    {
        private readonly StatusesRepository _statusesRepository;

        public StatusesService(StatusesRepository statusesRepository)
        {
            _statusesRepository = statusesRepository;
        }


        public async Task<List<Status>> GetStatusesAsync()
        {
            return await _statusesRepository.GetAllAsync();
        }

        public async Task AddStatusAsync(string status)
        {
            await _statusesRepository.CreateAsync(new Status() { Name = status });
            await _statusesRepository.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(StatusDto status)
        {
            var databaseStatus = await _statusesRepository.GetByIdAsync(status.Id);
            databaseStatus.Name = status.Name;

            _statusesRepository.Update(databaseStatus);
            await _statusesRepository.SaveChangesAsync();
        }
    }
}

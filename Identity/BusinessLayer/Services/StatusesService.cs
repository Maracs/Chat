using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services
{
    public class StatusesService: IStatusesService
    {
        private readonly StatusesRepository _statusesRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<StatusesService> _logger;

        public StatusesService(StatusesRepository statusesRepository,IMapper mapper, ILogger<StatusesService> logger)
        {
            _statusesRepository = statusesRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<List<StatusDto>> GetStatusesAsync(int limit,int offset)
        {
            _logger.LogInformation(
              "Trying to get statuses."
            );

            var statuses = _mapper.Map<List<StatusDto>>(await _statusesRepository.GetAllAsync(limit, offset));

            _logger.LogInformation(
              "Successfully get statuses."
            );

            return statuses;
        }

        public async Task AddStatusAsync(string status)
        {
            _logger.LogInformation(
              "Trying to add status."
            );

            await _statusesRepository.CreateAsync(new Status() { Name = status });
            await _statusesRepository.SaveChangesAsync();

            _logger.LogInformation(
              "Successfully add status."
            );
        }

        public async Task UpdateStatusAsync(StatusDto status)
        {
            _logger.LogInformation(
              "Trying to update status."
            );

            var databaseStatus = await _statusesRepository.GetByIdAsync(status.Id);
            databaseStatus.Name = status.Name;
            _statusesRepository.Update(databaseStatus);
            await _statusesRepository.SaveChangesAsync();

            _logger.LogInformation(
              "Successfully update status."
            );
        }
    }
}

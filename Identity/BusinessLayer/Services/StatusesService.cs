using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;


namespace BusinessLayer.Services
{
    public class StatusesService: IStatusesService
    {
        private readonly StatusesRepository _statusesRepository;

        private readonly IMapper _mapper;

        public StatusesService(StatusesRepository statusesRepository,IMapper mapper)
        {
            _statusesRepository = statusesRepository;
            _mapper = mapper;
        }


        public async Task<List<StatusDto>> GetStatusesAsync(int limit,int offset)
        {
            return _mapper.Map<List<StatusDto>>(await _statusesRepository.GetAllAsync(limit,offset));
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

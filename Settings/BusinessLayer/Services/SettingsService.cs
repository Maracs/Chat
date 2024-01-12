using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.Dtos;
using BusinessLayer.Exceptions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using static BusinessLayer.Exceptions.ApiException;

namespace BusinessLayer.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly SettingsRepository _settingsRepository;

        private readonly IMapper _mapper;

        public SettingsService(SettingsRepository settingsRepository, IMapper mapper)
        {
            _settingsRepository = settingsRepository;
            _mapper = mapper;
        }
        public async Task AddSettingAsync(SettingDto settingDto,int userId, CancellationToken token)
        {
            if (await _settingsRepository.IsEntityExistsAsync(userId.ToString(), token))
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var setting = _mapper.Map<SettingsInfo>(settingDto);
            setting.Id = userId.ToString();
            await _settingsRepository.CreateAsync(setting,token);
        }

        public async Task DeleteSettingAsync(int id, CancellationToken token)
        {
            if (!(await _settingsRepository.IsEntityExistsAsync(id.ToString(),token)))
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            await _settingsRepository.DeleteAsync(id.ToString(), token);
        }

        public async Task<SettingDto> GetSettingAsync(int id, CancellationToken token)
        {
            var setting = await _settingsRepository.GetByIdAsync(id.ToString(),token);

            if (setting is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            return _mapper.Map<SettingDto>(setting);
        }

        public async Task UpdateSettingAsync(SettingDto settingDto, int userId, CancellationToken token)
        {
            if (!(await _settingsRepository.IsEntityExistsAsync(userId.ToString(), token)))
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var setting = _mapper.Map<SettingsInfo>(settingDto);
            setting.Id = userId.ToString();
            await _settingsRepository.UpdateAsync(setting,token);
        }
    }
}

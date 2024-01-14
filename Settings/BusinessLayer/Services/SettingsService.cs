using Amazon.Runtime.Internal.Util;
using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.Dtos;
using BusinessLayer.Exceptions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Logging;
using static BusinessLayer.Exceptions.ApiException;

namespace BusinessLayer.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly SettingsRepository _settingsRepository;

        private readonly IMapper _mapper;

        private readonly ILogger<SettingsService> _logger;

        public SettingsService(SettingsRepository settingsRepository, IMapper mapper, ILogger<SettingsService> logger)
        {
            _settingsRepository = settingsRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task AddSettingAsync(SettingDto settingDto,int userId, CancellationToken token)
        {
            _logger.LogInformation("Trying to call AddSettingAsync.");

            if (await _settingsRepository.IsEntityExistsAsync(userId.ToString(), token))
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var setting = _mapper.Map<SettingsInfo>(settingDto);
            setting.Id = userId.ToString();
            await _settingsRepository.CreateAsync(setting,token);

            _logger.LogInformation("AddSettingAsync was called successfully.");
        }

        public async Task DeleteSettingAsync(int id, CancellationToken token)
        {
            _logger.LogInformation("Trying to call DeleteSettingAsync.");

            if (!(await _settingsRepository.IsEntityExistsAsync(id.ToString(),token)))
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            await _settingsRepository.DeleteAsync(id.ToString(), token);

            _logger.LogInformation("DeleteSettingAsync was called successfully.");
        }

        public async Task<SettingDto> GetSettingAsync(int id, CancellationToken token)
        {
            _logger.LogInformation("Trying to call GetSettingAsync.");

            var setting = await _settingsRepository.GetByIdAsync(id.ToString(),token);

            if (setting is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _logger.LogInformation("GetSettingAsync was called successfully.");

            return _mapper.Map<SettingDto>(setting);
        }

        public async Task UpdateSettingAsync(SettingDto settingDto, int userId, CancellationToken token)
        {
            _logger.LogInformation("Trying to call UpdateSettingAsync.");

            if (!(await _settingsRepository.IsEntityExistsAsync(userId.ToString(), token)))
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var setting = _mapper.Map<SettingsInfo>(settingDto);
            setting.Id = userId.ToString();
            await _settingsRepository.UpdateAsync(setting,token);

            _logger.LogInformation("UpdateSettingAsync was called successfully.");
        }
    }
}

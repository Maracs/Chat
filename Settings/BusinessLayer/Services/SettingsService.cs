using AutoMapper;
using BusinessLayer.Contracts;
using BusinessLayer.Dtos;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;


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
            var setting = _mapper.Map<SettingsInfo>(settingDto);
            setting.Id = userId.ToString();
            await _settingsRepository.CreateAsync(setting,token);
        }

        public async Task DeleteSettingAsync(int id, CancellationToken token)
        {
            await _settingsRepository.DeleteAsync(id.ToString(), token);
        }

        public async Task<SettingDto> GetSettingAsync(int id, CancellationToken token)
        {
            var setting = await _settingsRepository.GetByIdAsync(id.ToString(),token);

            return _mapper.Map<SettingDto>(setting);
        }

        public async Task UpdateSettingAsync(SettingDto settingDto, int userId, CancellationToken token)
        {
            var setting = _mapper.Map<SettingsInfo>(settingDto);
            setting.Id = userId.ToString();
            await _settingsRepository.UpdateAsync(setting,token);
        }
    }
}

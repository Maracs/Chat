using BusinessLayer.Dtos;

namespace BusinessLayer.Contracts
{
    public interface ISettingsService
    {
        public Task AddSettingAsync(SettingDto setting, int userId, CancellationToken token);

        public Task<SettingDto> GetSettingAsync(int id, CancellationToken token);

        public Task UpdateSettingAsync(SettingDto setting, int userId, CancellationToken token);

        public Task DeleteSettingAsync(int id, CancellationToken token);
    }
}

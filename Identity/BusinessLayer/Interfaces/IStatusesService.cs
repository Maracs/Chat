using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IStatusesService
    {
        public Task<List<StatusDto>> GetStatusesAsync(int limit, int offset);

        public Task AddStatusAsync(string status);

        public Task UpdateStatusAsync(StatusDto status);
    }
}

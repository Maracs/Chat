using BusinessLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    public interface IBlockingsService
    {
        public Task AddBlockingAsync(int userId, int bid);

        public Task DeleteBlockingAsync(int userId, int bid);

        public Task<List<BlockingDto>> GetBlockingsAsync(int userId,int offset,int limit);
    }
}

using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBlockingsService
    {
        public Task AddBlockingAsync(int userId, int bid);

        public Task DeleteBlockingAsync(int userId, int bid);

        public Task<List<BlockingDto>> GetBlockingsAsync(int userId,int offset,int limit);
    }
}

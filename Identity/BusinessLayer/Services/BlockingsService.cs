using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BlockingsService
    {
        private readonly BlockingsRepository _blockingsRepository;

        public BlockingsService(BlockingsRepository blockingsRepository)
        {
            _blockingsRepository = blockingsRepository;
        }
    }
}

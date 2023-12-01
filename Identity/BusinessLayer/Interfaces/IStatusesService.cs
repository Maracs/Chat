using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IStatusesService
    {
        public Task<List<StatusDto>> GetStatusesAsync(int limit, int offset);

        public Task AddStatusAsync(string status);

        public Task UpdateStatusAsync(StatusDto status);
    }
}

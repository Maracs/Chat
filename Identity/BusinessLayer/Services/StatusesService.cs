using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class StatusesService
    {
        private readonly StatusesRepository _statusesRepository;

        public StatusesService(StatusesRepository statusesRepository)
        {
            _statusesRepository = statusesRepository;
        }

    }
}

using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UsersService
    {
        private readonly UsersRepository _userRepository;

        public UsersService(UsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

       
    }
}

using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class FriendsService
    {
        private readonly FriendsRepository _friendsRepository;

        public FriendsService(FriendsRepository friendsRepository)
        {
            _friendsRepository = friendsRepository;
        }
    }
}

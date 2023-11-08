using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Friend
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int UserFriendId { get; set; }
        public User UserFriend { get; set; }
    }
}

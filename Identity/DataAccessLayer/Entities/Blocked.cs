using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Blocked
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int BlockedUserId { get; set; }
        public User BlockedUser { get; set; }

    }
}

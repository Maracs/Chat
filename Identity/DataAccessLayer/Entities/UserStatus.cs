using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class UserStatus
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}

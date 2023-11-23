using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ChatUser
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinTime { get; set; }
    }
}

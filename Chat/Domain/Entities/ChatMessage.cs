using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ChatMessage
    {
        public int ChatId { get; set; }
        public int MessageId {get; set; }
        public DateTime SendTime { get; set; }
        public int UserId { get; set; }
    }
}

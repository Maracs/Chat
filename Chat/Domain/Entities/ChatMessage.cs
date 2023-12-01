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
        public Chat Chat { get; set; } = null!;

        public int MessageId {get; set; }
        public Message Message { get; set; } = null!;

        public int MessageStatusId { get; set; }
        public MessageStatus MessageStatus { get; set; } = null!;

        public DateTime SendTime { get; set; }
        public int UserId { get; set; }
        
    }
}

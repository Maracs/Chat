using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MessageStatus
    {
        public const string Sending = "sending";
        public const string Read = "read";
        public const string Unread = "unread";

        public int Id { get; set; }
        public string Status { get; set; } = null!;

        public List<ChatMessage> Messages { get; set; } = null!;
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public ChatUser Creator { get; set; } = null!;

        public string? Info { get; set; }

        public List<ChatMessage> Messages { get; set; } = null!;

        public List<ChatUser> Users { get; set; } = null!;
    }
}

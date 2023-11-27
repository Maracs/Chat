using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UserChatDto
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
    }
}

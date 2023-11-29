using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateChatDto
    {
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public string? Info { get; set; }
    }
}

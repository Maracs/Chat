using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class BlockingDto
    {
        public int? UserId { get; set; }
        public string? UserNickName { get; set; }
        public string? AccountName { get; set; }
        public string? UserInfo { get; set; }
    }
}

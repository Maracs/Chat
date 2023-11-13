using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class RegisterDto
    {
        public string? AccountName { get; set; }
        public string? Password { get; set; }
        public string? Nickname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
    }
}

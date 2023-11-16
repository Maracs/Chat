using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class FullUserInfoWithoutIdDto
    {
        public string? UserNickName { get; set; }
        public string? AccountName { get; set; }
        public string? UserInfo { get; set; }
        public string? UserLastName { get; set; }
        public string? UserPhone { get; set; }
        public string? UserFirstName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string AccountName { get; set; }

        public string Passhash { get; set; }

        public string PasshashSalt { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<UserImage> UserImages { get; set; }

        public UserInfo UserInfo { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<UserStatus> UserStatuses { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Friend> Friends { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Blocked> BlockedUsers { get; set; }
    }
}

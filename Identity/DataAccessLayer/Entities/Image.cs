using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Src { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<UserImage> UsersImages { get; set; }
    }
}

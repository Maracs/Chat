using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class UserImage
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}

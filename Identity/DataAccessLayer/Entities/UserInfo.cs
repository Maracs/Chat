using System.ComponentModel.DataAnnotations;


namespace DataAccessLayer.Entities
{
    public class UserInfo
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Phone { get; set; }
        public string Nickname { get; set; }
        public string? Info { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;


namespace DataAccessLayer.Entities
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<UserStatus> UsersStatuses { get; set; }
    }
}

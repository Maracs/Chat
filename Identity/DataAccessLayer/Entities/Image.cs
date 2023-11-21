using System.ComponentModel.DataAnnotations;


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

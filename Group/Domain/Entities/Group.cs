namespace Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public string? Info { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public List<Post> Posts { get; set; } = null!;
        [System.Text.Json.Serialization.JsonIgnore]
        public List<GroupUser> Users { get; set; } = null!;
    }
}

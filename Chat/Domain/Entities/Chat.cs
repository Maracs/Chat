namespace Domain.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public string? Info { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public List<ChatMessage> Messages { get; set; } = null!;
        [System.Text.Json.Serialization.JsonIgnore]
        public List<ChatUser> Users { get; set; } = null!;
    }
}

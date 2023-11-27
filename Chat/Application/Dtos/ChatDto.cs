using Domain.Entities;


namespace Application.Dtos
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public string? Info { get; set; }
        public List<ChatMessage> Messages { get; set; } = null!;
        public List<ChatUser> Users { get; set; } = null!;
    }
}

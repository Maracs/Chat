using Domain.Entities;


namespace Application.Dtos
{
    public class ChatDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public string? Info { get; set; }
        public List<MessageDto> Messages { get; set; } = null!;
        public List<UserChatDto> Users { get; set; } = null!;
    }
}

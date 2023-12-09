namespace Application.Dtos
{
    public class CreateChatDto
    {
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public string? Info { get; set; }
    }
}

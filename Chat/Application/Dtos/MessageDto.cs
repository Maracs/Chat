namespace Application.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Content { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime SendTime { get; set; }
        public int UserId { get; set; }
    }
}

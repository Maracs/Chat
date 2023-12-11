namespace Application.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SendTime { get; set; }
        public List<string> Photos { get; set; }
    }
}

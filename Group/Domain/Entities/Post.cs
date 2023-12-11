namespace Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;

        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public DateTime SendTime { get; set; }

        public List<Photo> Photos { get; set; }
    }
}

namespace Domain.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Src { get; set; }=null!;

        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
    }
}

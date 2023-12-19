namespace Domain.Entities
{
    public class JoinRequest
    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public int UserId { get; set; }
    }
}

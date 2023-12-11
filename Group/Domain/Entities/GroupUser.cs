namespace Domain.Entities
{
    public class GroupUser
    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public int UserId { get; set; }
        public DateTime JoinTime { get; set; }
    }
}

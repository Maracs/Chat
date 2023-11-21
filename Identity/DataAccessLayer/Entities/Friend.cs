namespace DataAccessLayer.Entities
{
    public class Friend
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int UserFriendId { get; set; }
        public User UserFriend { get; set; }
    }
}

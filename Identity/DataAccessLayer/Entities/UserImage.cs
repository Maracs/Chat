namespace DataAccessLayer.Entities
{
    public class UserImage
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}

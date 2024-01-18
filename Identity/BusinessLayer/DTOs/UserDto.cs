namespace BusinessLayer.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? AccountName { get; set; }
        public string? Nickname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public bool IsOnline { get; set; }
    }
}

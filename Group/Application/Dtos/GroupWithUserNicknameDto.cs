namespace Application.Dtos
{
    public class GroupWithUserNicknameDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public string CreatorAccountName { get; set; }
        public string CreatorNickName { get; set; }
        public string? Info { get; set; }
        public List<PostDto> Posts { get; set; } = null!;
        public List<UserGroupDto> Users { get; set; } = null!;
    }
}

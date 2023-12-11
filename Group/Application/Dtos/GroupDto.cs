namespace Application.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public string? Info { get; set; }
        public List<PostDto> Posts { get; set; } = null!;
        public List<UserGroupDto> Users { get; set; } = null!;
    }
}

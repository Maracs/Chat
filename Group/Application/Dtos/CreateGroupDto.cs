namespace Application.Dtos
{
    public class CreateGroupDto
    {
        public string Name { get; set; } = null!;
        public int CreatorId { get; set; }
        public string? Info { get; set; }
    }
}

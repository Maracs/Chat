using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccessLayer.DataConfigurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role() { Id = 1, Name = "User" },
                new Role() { Id = 2, Name = "Admin" },
                new Role() { Id = 3, Name = "Moderator" }
            );
        }
    }
}

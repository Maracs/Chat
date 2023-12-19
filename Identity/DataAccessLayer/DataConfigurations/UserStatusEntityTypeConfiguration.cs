using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.DataConfigurations
{
    public class UserStatusEntityTypeConfiguration : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder
            .HasKey(userStatus => new { userStatus.UserId, userStatus.StatusId });
        }
    }
}

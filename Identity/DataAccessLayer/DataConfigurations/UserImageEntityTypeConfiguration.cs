using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccessLayer.DataConfigurations
{
    public class UserImageEntityTypeConfiguration : IEntityTypeConfiguration<UserImage>
    {
        public void Configure(EntityTypeBuilder<UserImage> builder)
        {
            builder
            .HasKey(userImage => new { userImage.UserId, userImage.ImageId });
        }
    }
}

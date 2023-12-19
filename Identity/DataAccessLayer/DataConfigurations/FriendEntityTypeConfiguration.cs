using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.DataConfigurations
{
    public class FriendEntityTypeConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder
            .HasKey(friend => new { friend.UserId, friend.UserFriendId });

            builder
            .HasOne(f => f.UserFriend)
            .WithMany()
            .HasForeignKey(f => f.UserFriendId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

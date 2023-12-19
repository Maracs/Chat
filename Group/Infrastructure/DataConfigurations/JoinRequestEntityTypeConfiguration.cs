using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataConfigurations
{
    public class JoinRequestEntityTypeConfiguration : IEntityTypeConfiguration<JoinRequest>
    {
        public void Configure(EntityTypeBuilder<JoinRequest> builder)
        {
            builder.HasKey(joinRequest => new { joinRequest.UserId, joinRequest.GroupId });
        }
    }
}

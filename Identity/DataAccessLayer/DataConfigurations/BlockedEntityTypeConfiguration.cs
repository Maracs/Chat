using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataConfigurations
{
    public class BlockedEntityTypeConfiguration:IEntityTypeConfiguration<Blocked>
    {
        public void Configure(EntityTypeBuilder<Blocked> builder)
        {
             builder
            .HasKey(blocked => new { blocked.UserId, blocked.BlockedUserId });

             builder
            .HasOne(b => b.BlockedUser)
            .WithMany()
            .HasForeignKey(b => b.BlockedUserId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

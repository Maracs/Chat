using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataConfigurations
{
    public class ChatMessageEntityTypeConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(chatMessage=>chatMessage.MessageId);

            builder
              .HasOne(b => b.Message)
              .WithMany()
              .HasForeignKey(b => b.MessageId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

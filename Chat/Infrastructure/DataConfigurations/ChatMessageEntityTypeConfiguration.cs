using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


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

using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccessLayer.DataConfigurations
{
    public class StatusEntityTypeConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(
                new Status() { Id = 1, Name = "Online"},
                new Status() { Id = 2, Name = "Offline" },
                new Status() { Id = 3, Name = "Busy" }
            ); 
        }
    }
}

using DataAccessLayer.DataConfigurations;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Data
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<Blocked> Blockeds { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserImage> UserImages { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<UserStatus> UserStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new BlockedEntityTypeConfiguration().Configure(modelBuilder.Entity<Blocked>());

            new FriendEntityTypeConfiguration().Configure(modelBuilder.Entity<Friend>());

            new UserImageEntityTypeConfiguration().Configure(modelBuilder.Entity<UserImage>());

            new UserStatusEntityTypeConfiguration().Configure(modelBuilder.Entity<UserStatus>());
        }

    }
}

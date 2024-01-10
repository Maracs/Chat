using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccessLayer.Data
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) 
        {
             Database.EnsureCreated();
        }

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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}

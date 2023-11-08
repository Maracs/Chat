using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            modelBuilder.Entity<Blocked>()
            .HasKey(blocked => new {blocked.UserId,blocked.BlockedUserId});

            modelBuilder.Entity<Blocked>()
            .HasOne(b => b.BlockedUser)
            .WithMany()
            .HasForeignKey(b => b.BlockedUserId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Friend>()
            .HasKey(friend => new {friend.UserId,friend.UserFriendId});

            modelBuilder.Entity<Friend>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.NoAction);


           

            modelBuilder.Entity<UserImage>()
            .HasKey(userImage => new { userImage.UserId, userImage.ImageId });

            modelBuilder.Entity<UserStatus>()
           .HasKey(userStatus => new { userStatus.UserId, userStatus.StatusId});


        }

    }
}

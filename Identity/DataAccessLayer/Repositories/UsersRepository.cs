using DataAccessLayer.Contracts;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UsersRepository : Repository<User>
    {
        public UsersRepository(DatabaseContext databaseContext)
        :base(databaseContext)
        {
            
        }

        public async Task<User> GetByAccountnameAsync(string? accountName)
        {
            var user = await _databaseContext.Users.FirstOrDefaultAsync(x => x.AccountName == accountName);
            if (user == null) { throw new NullReferenceException(); }
            return user;
        }

        public async Task<string> GetUserRoleAsync(User user)
        {
            var role = await _databaseContext.Roles.FindAsync(user.RoleId);
            if (role == null) { throw new NullReferenceException(); }
            return role.Name;
        }

        public async Task<bool> ifAccountExistsAsync(string accountName)
        {
            return await _databaseContext.Users.AnyAsync(x => x.AccountName == accountName);
        }

        public async Task CreateUserInfoAsync(UserInfo userInfo)
        {
            await _databaseContext.UserInfos.AddAsync(userInfo);
        }

        public async Task<User> GetUserWithInfo(int id)
        {
            return await _databaseContext.Users
                .Include(u=>u.UserInfo)
                .Include(u=>u.Role)
                .Where(u=>u.Id==id)
                .FirstAsync();
        }

        public async Task<List<UserStatus>> GetUserStatusesAsync(int id)
        {
            return await _databaseContext.UserStatuses
                .Include(u=>u.Status)
                .Where(u=>u.UserId == id).ToListAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _databaseContext.Users
                .Include(u=>u.UserInfo)
                .Include(u=>u.Role)
                .ToListAsync();
        }

        public async Task<User> GetProfileAsync(int id)
        {
            return await _databaseContext.Users
             .Include(u => u.UserInfo)
             .Include(u => u.Role)
             .Where(u=>u.Id==id)
             .FirstAsync();
        }

        public void UpdateInfo(UserInfo userInfo)
        {
            _databaseContext.UserInfos.Update(userInfo);
        }

        public async Task<List<UserImage>> GetUserPhotosAsync(int id)
        {
            return await _databaseContext.UserImages
               .Include(u => u.Image)
               .Where(u => u.UserId == id).ToListAsync();
        }

        public async Task AddPhotosAsync(int id, List<string> photosSrc)
        {
            foreach(var photoSrc in photosSrc) 
            {
                var entry = await _databaseContext.Images.AddAsync(new Image() { Src = photoSrc });
                await _databaseContext.SaveChangesAsync();
                await _databaseContext.UserImages.AddAsync(new UserImage() { UserId = id,ImageId = entry.Entity.Id });
            }
            
            await _databaseContext.SaveChangesAsync();
        }

        public async Task DeletePhotoAsync(int userId, int photoId)
        {
            _databaseContext.Remove(new UserImage() {UserId = userId,ImageId = photoId });
            _databaseContext.SaveChanges();
            var image = await _databaseContext.Images.FindAsync(photoId);
            _databaseContext.Remove(image);

            await _databaseContext.SaveChangesAsync();  
        }

        public async Task DeleteUserAsync(int id)
        {

            var userImages = await _databaseContext.UserImages.Include(u=>u.Image).Where(u => u.UserId == id).ToListAsync();

            foreach (var userImage in userImages)
                _databaseContext.Remove(userImage.Image);

            var userStatuses = await _databaseContext.UserStatuses.Where(u => u.UserId == id).ToListAsync();

            foreach (var status in userStatuses)
                 _databaseContext.Remove(status);

            var userInfos = await _databaseContext.UserInfos.Where(u => u.UserId == id).ToListAsync();

            foreach (var info in userInfos)
                _databaseContext.Remove(info);

            var userFriends = await _databaseContext.Friends.Where(u => u.UserId == id || u.UserFriendId == id).ToListAsync();

            foreach (var friend in userFriends)
                _databaseContext.Remove(friend);

            var userBlockings = await _databaseContext.Blockeds.Where(u => u.UserId == id || u.BlockedUserId == id).ToListAsync();

            foreach (var blocking in userBlockings)
                _databaseContext.Remove(blocking);

            var user = await GetByIdAsync(id);

            Delete(user);

            await _databaseContext.SaveChangesAsync();
        }

        public async Task AddUserStatusAsync(int id, int sid)
        {
            await _databaseContext.UserStatuses.AddAsync(new UserStatus() { UserId = id, StatusId = sid });
            await _databaseContext.SaveChangesAsync();
        }

        public async Task DeleteUserStatusAsync(int id, int sid)
        {
             _databaseContext.UserStatuses.Remove(new UserStatus() { UserId = id, StatusId = sid });
            await _databaseContext.SaveChangesAsync();
        }
    }
}

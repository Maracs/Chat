using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUsersService
    {
        public Task<TokenDto> SignUpAsync(SignupDto userDto);

        public  Task<TokenDto> SignInAsync(LoginDto loginDto);

        public Task<UserDto> GetUserAsync(int id);

        public Task<List<StatusDto>> GetUserStatusesAsync(int id);

        public Task<List<FullUserInfoDto>> GetAllUsersAsync(int offset, int limit);

        public Task<FullUserInfoDto> GetProfileAsync(int id);

        public Task UpdateProfileAsync(int id, FullUserInfoWithoutIdDto userDto);

        public Task DeleteUserAsync(int id);

        public Task UpdateRoleAsync(int id, string role);

        public Task<List<ImageDto>> GetPhotosAsync(int id);

        public Task AddPhotosAsync(int id, List<string> photosSrc);

        public Task DeletePhotosAsync(int userId, List<int> photosId);

        public Task AddUserStatusAsync(int id, int statusId);

        public Task DeleteUserStatusAsync(int id, int statusId);
    }
}

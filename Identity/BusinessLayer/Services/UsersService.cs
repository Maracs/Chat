using AutoMapper;
using Azure.Core;
using BusinessLayer.DTOs;
using BusinessLayer.Exeptions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UsersService
    {
        private readonly UsersRepository _userRepository;
        private readonly RolesRepository _rolesRepository;
        private readonly TokensService _tokenService;

        private readonly IMapper _mapper;


        public UsersService(UsersRepository userRepository, TokensService tokenService,
            RolesRepository rolesRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }


        public async Task<TokenDto> SignUpAsync(SignupDto userDto)
        {
            if (await _userRepository.ifAccountExistsAsync(userDto.AccountName))
                throw new ApiException("Account name is reserved", ApiException.ExceptionStatus.BadRequest);

            var roleId = await _rolesRepository.GetRoleIdAsync("User");

            var user = new User()
            {
                AccountName = userDto.AccountName,
                Passhash = userDto.Passhash,
                RoleId = roleId 
            };

            var userEntry = await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();


            var userInfo = new UserInfo()
            {
                Nickname = userDto.Nickname,
                Phone = userDto.PhoneNumber,
                UserId = userEntry.Entity.Id

            };

            await _userRepository.CreateUserInfoAsync(userInfo);
            await _userRepository.SaveChangesAsync();

            return new TokenDto()
            {
                AccessToken = await _tokenService.GetTokenAsync(userEntry.Entity)
            };
        }


        public async Task<TokenDto> SignInAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByAccountnameAsync(loginDto.AccountName);

            if (user == null)
                throw new ApiException("Account dont exists", ApiException.ExceptionStatus.NotFound);

            if (loginDto.Passhash != user.Passhash)
                throw new ApiException( "Invalid password or account name",ApiException.ExceptionStatus.Unauthorized);

            return new TokenDto()
            {
                AccessToken = await _tokenService.GetTokenAsync(user)
            };
        }


        public async Task<UserDto> GetUserAsync(int id)
        {
            var user = await _userRepository.GetUserWithInfo(id);

            return _mapper.Map<UserDto>(user);
        }


        public async Task<List<Status>> GetUserStatusesAsync(int id)
        {
           var userStatuses = await _userRepository.GetUserStatusesAsync(id);

            var statuses = new List<Status>();
            foreach (var userStatus in userStatuses)
                statuses.Add(userStatus.Status);
            return statuses;
        }


        public async Task<List<FullUserInfoDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            
            return _mapper.Map<List<FullUserInfoDto>>(users);
        }


        public async Task<FullUserInfoDto> GetProfileAsync(int id)
        {
            var user = await _userRepository.GetProfileAsync(id);

            return  _mapper.Map<FullUserInfoDto>(user);
        }


        public async Task UpdateProfileAsync(int id, FullUserInfoWithoutIdDto userDto)
        {
            var user = await _userRepository.GetProfileAsync(id);

            user.AccountName = userDto.AccountName;
            user.UserInfo.Nickname = userDto.UserNickName;
            user.UserInfo.Info = userDto.UserInfo;
            user.UserInfo.LastName = userDto.UserLastName;
            user.UserInfo.Phone = userDto.UserPhone;
            user.UserInfo.FirstName = userDto.UserFirstName;

            _userRepository.Update(user);

            _userRepository.UpdateInfo(user.UserInfo);

            await _userRepository.SaveChangesAsync();
        }


        public async Task DeleteUserAsync(int id)
        {
            
            await _userRepository.DeleteUserAsync(id);
        }


        public async Task UpdateRoleAsync(int id, string role)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var roleId = await _rolesRepository.GetRoleIdAsync(role);

            user.RoleId = roleId;

            _userRepository.Update(user);

            await _userRepository.SaveChangesAsync();

        }

        public async Task<List<Image>> GetPhotosAsync(int id)
        {
            var userPhotos = await _userRepository.GetUserPhotosAsync(id);

            var photoes = new List<Image>();
            foreach (var userPhoto in userPhotos)
                photoes.Add(new Image() {Id = userPhoto.ImageId,Src = userPhoto.Image.Src });
            return photoes;
        }

        public async Task AddPhotosAsync(int id,List<string> photosSrc)
        {
            await _userRepository.AddPhotosAsync(id,photosSrc);
        }

        public async Task DeletePhotosAsync(int userId, List<int> photosId)
        {

            foreach (var photoId in photosId)
                await  _userRepository.DeletePhotoAsync(userId,photoId);
            
        }

        public async Task AddUserStatusAsync(int id, int sid)
        {
            await _userRepository.AddUserStatusAsync(id,sid);
        }

        public async Task DeleteUserStatusAsync(int id, int sid)
        {
            await _userRepository.DeleteUserStatusAsync( id,  sid);
        }
    }
}

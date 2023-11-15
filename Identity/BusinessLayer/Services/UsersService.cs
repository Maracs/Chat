using Azure.Core;
using BusinessLayer.DTOs;
using BusinessLayer.Exeptions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
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

        public UsersService(UsersRepository userRepository, TokensService tokenService, RolesRepository rolesRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _rolesRepository = rolesRepository;
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
                AccessToken = await _tokenService.GetTokenAsync(user)
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
    }
}

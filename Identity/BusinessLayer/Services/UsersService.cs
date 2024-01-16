using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;

namespace BusinessLayer.Services
{
    
    public class UsersService: IUsersService
    {
        private readonly UsersRepository _userRepository;
        private readonly RolesRepository _rolesRepository;
        private readonly ITokensService _tokenService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersService> _logger;


        public UsersService(UsersRepository userRepository, ITokensService tokenService,
            RolesRepository rolesRepository,IMapper mapper, IPublishEndpoint publishEndpoint, ILogger<UsersService> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }


        public async Task<TokenDto> SignUpAsync(SignupDto userDto)
        {
            _logger.LogInformation(
              "Trying to SignUp user."
            );

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

            _logger.LogInformation(
              "SignUp user was successfully."
            );

            return new TokenDto()
            {
                AccessToken = await _tokenService.GetTokenAsync(userEntry.Entity)
            };
        }


        public async Task<TokenDto> SignInAsync(LoginDto loginDto)
        {
            _logger.LogInformation(
              "User trying to SignIn."
            );

            var user = await _userRepository.GetByAccountnameAsync(loginDto.AccountName);

            if (user == null)
            {
                throw new ApiException("Account dont exists", ApiException.ExceptionStatus.NotFound);
            }
               

            if (loginDto.Passhash != user.Passhash)
            {
                throw new ApiException("Invalid password or account name", ApiException.ExceptionStatus.Unauthorized);
            }

            _logger.LogInformation(
              "SignIn was successfully."
            );

            return new TokenDto()
            {
                AccessToken = await _tokenService.GetTokenAsync(user)
            };
        }


        public async Task<UserDto> GetUserAsync(int id)
        {
            _logger.LogInformation(
              "Trying to Get user with id {Id}.",
              id
            );

            var user = await _userRepository.GetUserWithInfo(id);

            _logger.LogInformation(
              "Get User with id {Id} was successfully.",
              id
            );

            return _mapper.Map<UserDto>(user);
        }


        public async Task<List<StatusDto>> GetUserStatusesAsync(int id)
        {
            _logger.LogInformation(
              "Trying to call GetUserStatusesAsync."
            );

            var userStatuses = await _userRepository.GetUserStatusesAsync(id);

            var statuses = userStatuses
                .Select(status => _mapper.Map<StatusDto>(status.Status))
                .ToList();

            _logger.LogInformation(
              "GetUserStatusesAsync was called successfully."
            );

            return statuses;
        }


        public async Task<List<FullUserInfoDto>> GetAllUsersAsync(int offset, int limit)
        {
            _logger.LogInformation(
              "Trying to call GetAllUsersAsync."
            );

            var users = await _userRepository.GetAllUsersAsync(offset,limit);

            _logger.LogInformation(
              "GetAllUsersAsync was called successfully."
            );

            return _mapper.Map<List<FullUserInfoDto>>(users);
        }


        public async Task<FullUserInfoDto> GetProfileAsync(int id)
        {
            _logger.LogInformation(
               "Trying to Get user profile with id {Id}.",
               id
             );

            var user = await _userRepository.GetProfileAsync(id);

            _logger.LogInformation(
            "Get user profile with id {Id} was successfully.",
               id
             );

            return  _mapper.Map<FullUserInfoDto>(user);
        }


        public async Task UpdateProfileAsync(int id, FullUserInfoWithoutIdDto userDto)
        {
            _logger.LogInformation(
               "Trying to Update user profile with id {Id}.",
               id
             );

            var user = await _userRepository.GetProfileAsync(id);

            user.AccountName = userDto.AccountName;

            var userInfoId = user.UserInfo.Id;
            user.UserInfo = _mapper.Map<UserInfo>(userDto);
            user.UserInfo.Id = userInfoId;

            _userRepository.Update(user);

            _userRepository.UpdateInfo(user.UserInfo);

            await _userRepository.SaveChangesAsync();

            _logger.LogInformation(
            "Update user profile with id {Id} was successfully.",
              id
            );
        }


        public async Task DeleteUserAsync(int id)
        {
            _logger.LogInformation(
              "Trying to Delete user  with id {Id}.",id
            );

            await _userRepository.DeleteUserAsync(id);
            await _publishEndpoint.Publish(new UserIdForGroupDto {UserId = id });
            await _publishEndpoint.Publish(new UserIdForChatDto { UserId = id });

            _logger.LogInformation(
              "Delete User with id {Id} was called successfully.",id
            );
        }


        public async Task UpdateRoleAsync(int id, string role)
        {
            _logger.LogInformation(
              "Trying to  Update Role of User with id {Id}.",id
            );

            var user = await _userRepository.GetByIdAsync(id);

            var roleId = await _rolesRepository.GetRoleIdAsync(role);

            user.RoleId = roleId;

            _userRepository.Update(user);

            await _userRepository.SaveChangesAsync();

            _logger.LogInformation(
              "Update Role of User with id {Id} was successfully.",id
            );
        }

        public async Task<List<ImageDto>> GetPhotosAsync(int id)
        {
            _logger.LogInformation(
              "Trying to call Get Photos of User with id {Id}.",id
            );

            var userPhotos = await _userRepository.GetUserPhotosAsync(id);

            var photoes = userPhotos.Select(photo=>_mapper.Map<ImageDto>(photo.Image)).ToList();

            _logger.LogInformation(
              "Get Photos of User with id {Id} was called successfully.",id
            );

            return photoes;
        }

        public async Task AddPhotosAsync(int id,List<string> photosSrc)
        {
            _logger.LogInformation(
              "Trying to Add Photos of User with id {Id}.",id
            );

            await _userRepository.AddPhotosAsync(id,photosSrc);

            _logger.LogInformation(
              "Add Photos of User with id {Id} was called successfully.",id
            );
        }

        public async Task DeletePhotosAsync(int userId, List<int> photosId)
        {
            _logger.LogInformation(
              "Trying to Delete Photos of User with id {Id}.",userId
            );

            photosId.ForEach( photoId => _userRepository.DeletePhoto(userId, photoId));

            _logger.LogInformation(
              "Delete Photos of User with id {Id} was called successfully.",userId
            );
        }

        public async Task AddUserStatusAsync(int id, int statusId)
        {
            _logger.LogInformation(
              "Trying to Add Status of User with id {Id}.",id
            );

            await _userRepository.AddUserStatusAsync(id, statusId);

            _logger.LogInformation(
              "Add Status of User with id {Id} was called successfully.",id
            );
        }

        public async Task DeleteUserStatusAsync(int id, int statusId)
        {
            _logger.LogInformation(
              "Trying to Delete Status of User with id {Id}.",id
            );

            await _userRepository.DeleteUserStatusAsync( id, statusId);

            _logger.LogInformation(
              "Delete Status of User with id {Id} was called successfully.",id
            );
        }
    }
}

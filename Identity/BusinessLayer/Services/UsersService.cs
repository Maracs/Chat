using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Exceptions;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using MassTransit;
using System.Linq.Expressions;

namespace BusinessLayer.Services
{
    
    public class UsersService: IUsersService
    {
        private readonly UsersRepository _userRepository;
        private readonly RolesRepository _rolesRepository;
        private readonly ITokensService _tokenService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;


        public UsersService(UsersRepository userRepository, ITokensService tokenService,
            RolesRepository rolesRepository,IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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
            {
                throw new ApiException("Account dont exists", ApiException.ExceptionStatus.NotFound);
            }
               

            if (loginDto.Passhash != user.Passhash)
            {
                throw new ApiException("Invalid password or account name", ApiException.ExceptionStatus.Unauthorized);
            }
                

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


        public async Task<List<StatusDto>> GetUserStatusesAsync(int id)
        {
           var userStatuses = await _userRepository.GetUserStatusesAsync(id);

            var statuses = userStatuses
                .Select(status => _mapper.Map<StatusDto>(status.Status))
                .ToList();

            return statuses;
        }


        public async Task<List<FullUserInfoDto>> GetAllUsersAsync(int offset, int limit)
        {
            var users = await _userRepository.GetAllUsersAsync(offset,limit);
            
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

            var userInfoId = user.UserInfo.Id;
            user.UserInfo = _mapper.Map<UserInfo>(userDto);
            user.UserInfo.Id = userInfoId;

            _userRepository.Update(user);

            _userRepository.UpdateInfo(user.UserInfo);

            await _userRepository.SaveChangesAsync();
        }


        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            await _publishEndpoint.Publish(new UserIdForGroupDto {UserId = id });
            await _publishEndpoint.Publish(new UserIdForChatDto { UserId = id });
        }


        public async Task UpdateRoleAsync(int id, string role)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var roleId = await _rolesRepository.GetRoleIdAsync(role);

            user.RoleId = roleId;

            _userRepository.Update(user);

            await _userRepository.SaveChangesAsync();
        }

        public async Task<List<ImageDto>> GetPhotosAsync(int id)
        {
            var userPhotos = await _userRepository.GetUserPhotosAsync(id);

            var photoes = userPhotos.Select(photo=>_mapper.Map<ImageDto>(photo.Image)).ToList();

            return photoes;
        }

        public async Task AddPhotosAsync(int id,List<string> photosSrc)
        {
            await _userRepository.AddPhotosAsync(id,photosSrc);
        }

        public async Task DeletePhotosAsync(int userId, List<int> photosId)
        {
             photosId.ForEach( photoId => _userRepository.DeletePhoto(userId, photoId));
        }

        public async Task AddUserStatusAsync(int id, int statusId)
        {
            await _userRepository.AddUserStatusAsync(id, statusId);
        }

        public async Task DeleteUserStatusAsync(int id, int statusId)
        {
            await _userRepository.DeleteUserStatusAsync( id, statusId);
        }
    }
}

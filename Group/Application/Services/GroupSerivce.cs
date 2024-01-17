using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Grpc.Dtos;
using Grpc.Interfaces;
using Microsoft.Extensions.Logging;
using System;


namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IUserNicknameService _userNicknameService;
        private readonly ILogger<GroupService> _logger;

        public GroupService(IGroupRepository groupRepository, IMapper mapper, ILogger<GroupService> logger, IUserNicknameService userNicknameService)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _logger = logger;
            _userNicknameService = userNicknameService;
        }

        public async Task CreateAsync(int userId, CreateGroupDto groupDto, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Create Group.", userId);

            if (userId != groupDto.CreatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var group = _mapper.Map<Group>(groupDto);
            await _groupRepository.CreateAsync(group);
            await _groupRepository.SaveChangesAsync(token);

            _logger.LogInformation("User with id {UserId} successfully Create Group.", userId);
        }

        public async Task DeleteAsync(int userId, int id, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Delete Group with id {Id}.", userId, id);

            var group = await _groupRepository.GetByIdAsync(id,token);

            if(group is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }


            if (group.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _groupRepository.Delete(id);
            await _groupRepository.SaveChangesAsync(token);

            _logger.LogInformation("User with id {UserId} successfully Delete Group with id {Id}.", userId, id);
        }

        public async Task<List<GroupDto>> GetAllAsync(int userId, int offset, int limit, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Get All Groups.", userId);

            var groups = _mapper.Map<List<GroupDto>>(await _groupRepository.GetAllAsync(userId, offset, limit, token));

            _logger.LogInformation("User with id {UserId} Get all Groups successfully.", userId);

            return groups;
        }

        public async Task<GroupWithUserNicknameDto> GetByIdAsync(int userId, int id, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Get Group with id {Id}.", userId, id);

            var group = await _groupRepository.GetByIdAsync(id,token);

            if (group is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            if (group.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            var userNickname = await _userNicknameService.GetUserNicknameAsync(new UserIdDto() { Id = userId });
            var groupDto = _mapper.Map<GroupWithUserNicknameDto>(group);
            groupDto = _mapper.Map(userNickname, groupDto);
            _logger.LogInformation("User with id {UserId} Get Group with id {Id} successfully.", userId, id);

            return groupDto;
        }

        public async Task UpdateAsync(int userId, int id, CreateGroupDto groupDto, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Update Group with id {Id}.", userId, id);

            var group = await _groupRepository.GetByIdAsync(id,token);

            if (group is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            if (group.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            _mapper.Map<CreateGroupDto, Group>(groupDto, group);
            _groupRepository.Update(group);
            await _groupRepository.SaveChangesAsync(token);

            _logger.LogInformation("User with id {UserId} Update Group with id {Id} successfully.", userId, id);
        }
    }
}

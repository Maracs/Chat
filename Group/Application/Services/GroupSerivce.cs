using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared;
using System;

namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IUserNicknameService _userNicknameService;

        public GroupService(IGroupRepository groupRepository, IMapper mapper, IUserNicknameService userNicknameService)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _userNicknameService = userNicknameService;
        }

        public async Task CreateAsync(int userId, CreateGroupDto groupDto, CancellationToken token)
        {
            if (userId != groupDto.CreatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var group = _mapper.Map<Group>(groupDto);
            await _groupRepository.CreateAsync(group);
            await _groupRepository.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(int userId, int id, CancellationToken token)
        {
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
        }

        public async Task<List<GroupDto>> GetAllAsync(int userId, int offset, int limit, CancellationToken token)
        {
            return _mapper.Map<List<GroupDto>>(await _groupRepository.GetAllAsync(userId, offset, limit, token));
        }

        public async Task<GroupWithUserNicknameDto> GetByIdAsync(int userId, int id, CancellationToken token)
        {
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

            return groupDto;
        }

        public async Task UpdateAsync(int userId, int id, CreateGroupDto groupDto, CancellationToken token)
        {
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
        }
    }
}

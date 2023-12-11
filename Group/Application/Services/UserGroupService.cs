﻿using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IGroupRepository _groupRepository;

        public UserGroupService(IUserGroupRepository userGroupRepository, IGroupRepository groupRepository)
        {
            _userGroupRepository = userGroupRepository;
            _groupRepository = groupRepository;
        }

        public async Task AddAsync(int userId, UserGroupDto userGroupDto, CancellationToken token)
        {
            var creatorId = (await _groupRepository.GetByIdAsync(userGroupDto.GroupId)).CreatorId;

            if (userId != creatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            await _userGroupRepository.AddAsync(new GroupUser() { GroupId = userGroupDto.GroupId, UserId = userGroupDto.UserId, JoinTime = DateTime.Now });
            token.ThrowIfCancellationRequested();
            await _userGroupRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId, UserGroupDto userGroupDto, CancellationToken token)
        {
            var creatorId = (await _groupRepository.GetByIdAsync(userGroupDto.GroupId)).CreatorId;

            if (userId != creatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _userGroupRepository.Delete(new GroupUser() { GroupId = userGroupDto.GroupId, UserId = userGroupDto.UserId });
            token.ThrowIfCancellationRequested();
            await _userGroupRepository.SaveChangesAsync();
        }

        public async Task RequestAsync(int userId, UserGroupDto userGroupDto, CancellationToken token)
        {
            var creatorId = (await _groupRepository.GetByIdAsync(userGroupDto.GroupId)).CreatorId;

            if (userId != creatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            await _userGroupRepository.RequestAsync(new JoinRequest() { GroupId = userGroupDto.GroupId, UserId = userGroupDto.UserId});
            token.ThrowIfCancellationRequested();
            await _userGroupRepository.SaveChangesAsync();
        }
    }
}

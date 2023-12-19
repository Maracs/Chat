using Application.Dtos;
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
            var group = await _groupRepository.GetByIdAsync(userGroupDto.GroupId,token);

            if (group is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var creatorId = group.CreatorId;

            if (userId != creatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            await _userGroupRepository.AddAsync(new GroupUser() { GroupId = userGroupDto.GroupId, UserId = userGroupDto.UserId, JoinTime = DateTime.Now });
            await _userGroupRepository.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(int userId, UserGroupDto userGroupDto, CancellationToken token)
        {
            var group = await _groupRepository.GetByIdAsync(userGroupDto.GroupId,token);

            if (group is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var creatorId = group.CreatorId;

            if (userId != creatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _userGroupRepository.Delete(new GroupUser() { GroupId = userGroupDto.GroupId, UserId = userGroupDto.UserId });
            await _userGroupRepository.SaveChangesAsync(token);
        }

        public async Task RequestAsync(int userId, UserGroupDto userGroupDto, CancellationToken token)
        {
            var group = await _groupRepository.GetByIdAsync(userGroupDto.GroupId,token);

            if (group is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var creatorId = group.CreatorId;

            if (userId != creatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            await _userGroupRepository.RequestAsync(new JoinRequest() { GroupId = userGroupDto.GroupId, UserId = userGroupDto.UserId});
            await _userGroupRepository.SaveChangesAsync(token);
        }
    }
}

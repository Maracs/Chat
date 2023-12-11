using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;

namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(int userId, CreateGroupDto groupDto, CancellationToken token)
        {
            if (userId != groupDto.CreatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var user = new Group()
            {
                Name = groupDto.Name,
                CreatorId = groupDto.CreatorId,
                Info = groupDto.Info,
            };
            await _groupRepository.CreateAsync(user);
            token.ThrowIfCancellationRequested();
            await _groupRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId, int id, CancellationToken token)
        {
            var group = await _groupRepository.GetByIdAsync(id);

            if (group.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _groupRepository.Delete(id);
            token.ThrowIfCancellationRequested();
            await _groupRepository.SaveChangesAsync();
        }

        public async Task<List<GroupDto>> GetAllAsync(int userId, int offset, int limit, CancellationToken token)
        {
            return _mapper.Map<List<GroupDto>>(await _groupRepository.GetAllAsync(userId, offset, limit));
        }

        public async Task<GroupDto> GetByIdAsync(int userId, int id, CancellationToken token)
        {
            var group = await _groupRepository.GetByIdAsync(id);

            if (group.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            token.ThrowIfCancellationRequested();

            return _mapper.Map<GroupDto>(group);
        }

        public async Task UpdateAsync(int userId, int id, CreateGroupDto groupDto, CancellationToken token)
        {
            var group = await _groupRepository.GetByIdAsync(id);

            if (group.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            _mapper.Map<CreateGroupDto, Group>(groupDto, group);
            _groupRepository.Update(group);
            token.ThrowIfCancellationRequested();
            await _groupRepository.SaveChangesAsync();
        }
    }
}

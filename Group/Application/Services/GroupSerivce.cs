﻿using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GroupService> _logger;

        public GroupService(IGroupRepository groupRepository, IMapper mapper, ILogger<GroupService> logger)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(int userId, CreateGroupDto groupDto, CancellationToken token)
        {
            _logger.LogInformation(
              "Trying to call CreateAsync."
            );

            if (userId != groupDto.CreatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var group = _mapper.Map<Group>(groupDto);
            await _groupRepository.CreateAsync(group);
            await _groupRepository.SaveChangesAsync(token);

            _logger.LogInformation(
                 "CreateAsync was called successfully."
               );
        }

        public async Task DeleteAsync(int userId, int id, CancellationToken token)
        {
            _logger.LogInformation("Trying to call DeleteAsync.");

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

            _logger.LogInformation("DeleteAsync was called successfully.");
        }

        public async Task<List<GroupDto>> GetAllAsync(int userId, int offset, int limit, CancellationToken token)
        {
            _logger.LogInformation("Trying to call GetAllAsync.");

            var groups = _mapper.Map<List<GroupDto>>(await _groupRepository.GetAllAsync(userId, offset, limit, token));

            _logger.LogInformation("GetAllAsync was called successfully.");

            return groups;
        }

        public async Task<GroupDto> GetByIdAsync(int userId, int id, CancellationToken token)
        {
            _logger.LogInformation("Trying to call GetByIdAsync.");

            var group = await _groupRepository.GetByIdAsync(id,token);

            if (group is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            if (group.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            _logger.LogInformation("GetByIdAsync was called successfully.");

            return _mapper.Map<GroupDto>(group);
        }

        public async Task UpdateAsync(int userId, int id, CreateGroupDto groupDto, CancellationToken token)
        {
            _logger.LogInformation("Trying to call UpdateAsync.");

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

            _logger.LogInformation("UpdateAsync was called successfully.");
        }
    }
}

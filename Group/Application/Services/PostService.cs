﻿using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int userId, int groupId, int id, CancellationToken token)
        {
            var group = await _groupRepository.GetByIdAsync(groupId,token);

            if (group is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var user = group.Users
                           .Where(user => user.UserId == userId)
                           .FirstOrDefault();

            if (null == user && group.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _postRepository.Delete(groupId, id);
            await _postRepository.SaveChangesAsync(token);
        }

        public async Task<List<PostDto>> GetAllAsync(int userId, int chatid, int offset, int limit, CancellationToken token)
        {
            var posts = await _postRepository.GetAllAsync(userId, chatid, offset, limit,token);

            return _mapper.Map<List<PostDto>>(posts);
        }

        public async Task SendAsync(int userId, PostDto postDto, CancellationToken token)
        {
            var chat = await _groupRepository.GetByIdAsync(postDto.GroupId,token);

            if (chat is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            if (userId != chat.CreatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var post = _mapper.Map<Post>(postDto);

            await _postRepository.SendAsync(post, postDto.Photos);
            await _postRepository.SaveChangesAsync(token);
        }

        public async Task UpdateAsync(int userId, int groupId, int id, string content, CancellationToken token)
        {
            var group = await _groupRepository.GetByIdAsync(groupId,token);

            if (group is null)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var user = group.Users
                           .Where(user => user.UserId == userId)
                           .FirstOrDefault();

            if (null == user && group.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var post = await _postRepository.GetByIdAsync(groupId, id);
            post.Content = content;
            _postRepository.Update(post);
            await _postRepository.SaveChangesAsync(token);
        }
    }
}
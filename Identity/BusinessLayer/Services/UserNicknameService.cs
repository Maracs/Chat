﻿using AutoMapper;
using DataAccessLayer.Repositories;
using Grpc.DTOs;
using Grpc.Interfaces;

namespace BusinessLayer.Services
{
    public class UserNicknameService : IUserNicknameService
    {
        private readonly UsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UserNicknameService(UsersRepository usersRepository,IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<UserNicknameDto> GetUserNicknameAsync(UserIdDto userId)
        {
            var user = await _usersRepository.GetProfileAsync(userId.Id);
            var userNicknameDto = _mapper.Map<UserNicknameDto>(user);

            return userNicknameDto;
        }
    }
}

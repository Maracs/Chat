using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace BusinessLayer.Services
{
    public class FriendsService: IFriendsService
    {
        private readonly FriendsRepository _friendsRepository;
        private readonly BlockingsRepository _blockingsRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<FriendsService> _logger;

        public FriendsService(FriendsRepository friendsRepository,BlockingsRepository blockingsRepository,IMapper mapper,ILogger<FriendsService> logger)
        {
            _friendsRepository = friendsRepository;
            _blockingsRepository = blockingsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddFriendAsync(int userId, int fid)
        {
            _logger.LogInformation(
                "User with ID {UserId} trying to add friend user with ID {Fid}.",
                userId,
                fid
            );

            if (! await _blockingsRepository.IfExists(userId, fid))
            {
                _logger.LogInformation(
                    "User with ID {UserId} successfully add friend user with ID {Fid}.",
                    userId,
                    fid
                );

                await _friendsRepository.CreateAsync(new Friend() { UserId = userId, UserFriendId = fid });
                await _friendsRepository.SaveChangesAsync();
            }      
        }

        public async Task DeleteFriendAsync(int userId, int fid)
        {
            _logger.LogInformation(
                 "User with ID {UserId} trying to delete friend user with ID {Fid}.",
                 userId,
                 fid
             );

            _friendsRepository.Delete(new Friend() { UserId = userId, UserFriendId = fid });
            await _friendsRepository.SaveChangesAsync();

            _logger.LogInformation(
                "User with ID {UserId} successfully delete friend user with ID {Fid}.",
                userId,
                fid
            );
        }

        public async Task<List<FullUserInfoDto>> GetFriendsAsync(int id,int offset,int limit)
        {
            _logger.LogInformation(
                 "User with ID {UserId} trying to get friends.",
                 id
             );

            var users = await _friendsRepository.GetFriendsAsync(id,offset,limit);

            _logger.LogInformation(
                 "User with ID {UserId} successfully get friends.",
                 id
             );

            return _mapper.Map<List<FullUserInfoDto>>(users);
        }
    }
}

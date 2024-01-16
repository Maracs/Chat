using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services
{
    public class BlockingsService: IBlockingsService
    {
        private readonly BlockingsRepository _blockingsRepository;

        private readonly FriendsRepository _friendsRepository;

        private readonly IMapper _mapper;

        private readonly ILogger<BlockingsService> _logger;

        public BlockingsService(BlockingsRepository blockingsRepository,FriendsRepository friendsRepository,IMapper mapper, ILogger<BlockingsService> logger)
        {
            _blockingsRepository = blockingsRepository;
            _friendsRepository = friendsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddBlockingAsync(int userId, int bid)
        {
            _logger.LogInformation(
                "User with ID {UserId} trying to block user with ID {Bid}.",
                userId,
                bid
            );

            if (await _friendsRepository.IfExists(userId, bid))
            {
                _friendsRepository.Delete(new Friend() { UserId = userId, UserFriendId = bid });
            }
                 
            await _blockingsRepository.CreateAsync(new Blocked() {UserId = userId, BlockedUserId = bid });
            await _blockingsRepository.SaveChangesAsync();

            _logger.LogInformation(
                "User with ID {UserId} successfully blocked user with ID {Bid}.",
                userId,
                bid
            );
        }

        public async Task DeleteBlockingAsync(int userId, int bid)
        {
            _logger.LogInformation(
                "User with ID {UserId} trying to delete blocking of user with ID {Bid}.",
                userId,
                bid
            );

            _blockingsRepository.Delete(new Blocked() { UserId = userId,BlockedUserId = bid});
            await _blockingsRepository.SaveChangesAsync();

            _logger.LogInformation(
                "User with ID {UserId} successfully delete blocking of user with ID {Bid}.",
                userId,
                bid
            );
        }

        public async Task<List<BlockingDto>> GetBlockingsAsync(int userId,int offset,int limit)
        {
            _logger.LogInformation(
                "User with ID {UserId} trying to get blockings.",
                userId
            );

            var blockigs = await _blockingsRepository.GetBlockingsAsync(userId, offset,limit);

            _logger.LogInformation(
                "User with ID {UserId} successfully get blockings.",
                userId
            );

            return _mapper.Map<List<BlockingDto>>(blockigs);
           
        }
    }
}

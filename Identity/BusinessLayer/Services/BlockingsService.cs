using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;


namespace BusinessLayer.Services
{
    public class BlockingsService: IBlockingsService
    {
        private readonly BlockingsRepository _blockingsRepository;

        private readonly FriendsRepository _friendsRepository;

        private readonly IMapper _mapper;

        public BlockingsService(BlockingsRepository blockingsRepository,FriendsRepository friendsRepository,IMapper mapper)
        {
            _blockingsRepository = blockingsRepository;
            _friendsRepository = friendsRepository;
            _mapper = mapper;
        }

        public async Task AddBlockingAsync(int userId, int bid)
        {
            if (await _friendsRepository.IfExists(userId, bid))
            {
                _friendsRepository.Delete(new Friend() { UserId = userId, UserFriendId = bid });
            }
                 
            await _blockingsRepository.CreateAsync(new Blocked() {UserId = userId, BlockedUserId = bid });
            await _blockingsRepository.SaveChangesAsync();
        }

        public async Task DeleteBlockingAsync(int userId, int bid)
        {
            _blockingsRepository.Delete(new Blocked() { UserId = userId,BlockedUserId = bid});
            await _blockingsRepository.SaveChangesAsync();
        }

        public async Task<List<BlockingDto>> GetBlockingsAsync(int userId,int offset,int limit)
        {
            var blockigs = await _blockingsRepository.GetBlockingsAsync(userId, offset,limit);

            return _mapper.Map<List<BlockingDto>>(blockigs);
           
        }
    }
}

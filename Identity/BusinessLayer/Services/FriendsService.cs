using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;


namespace BusinessLayer.Services
{
    public class FriendsService: IFriendsService
    {
        private readonly FriendsRepository _friendsRepository;
        private readonly BlockingsRepository _blockingsRepository;

        private readonly IMapper _mapper;

        public FriendsService(FriendsRepository friendsRepository,BlockingsRepository blockingsRepository,IMapper mapper)
        {
            _friendsRepository = friendsRepository;
            _blockingsRepository = blockingsRepository;
            _mapper = mapper;
        }

        public async Task AddFriendAsync(int userId, int fid)
        {
            if(! await _blockingsRepository.IfExists(userId, fid))
            {

                await _friendsRepository.CreateAsync(new Friend() { UserId = userId, UserFriendId = fid });
                await _friendsRepository.SaveChangesAsync();
            }      
        }

        public async Task DeleteFriendAsync(int userId, int fid)
        {
            
            _friendsRepository.Delete(new Friend() { UserId = userId, UserFriendId = fid });
            await _friendsRepository.SaveChangesAsync();
        }

        public async Task<List<FullUserInfoDto>> GetFriendsAsync(int id,int offset,int limit)
        {
            var users = await _friendsRepository.GetFriendsAsync(id,offset,limit);
       
           return _mapper.Map<List<FullUserInfoDto>>(users);
        }
    }
}

using AutoMapper;
using BusinessLayer.DTOs;
using DataAccessLayer.Entities;


namespace BusinessLayer.AutoMapperProfiles
{
    public class BlockingsProfile:Profile
    {
        public BlockingsProfile()
        {
            CreateMap<Blocked, BlockingDto>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.BlockedUserId))
               .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => src.BlockedUser.UserInfo.Info))
               .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.BlockedUser.AccountName))
               .ForMember(dest => dest.UserNickName, opt => opt.MapFrom(src => src.BlockedUser.UserInfo.Nickname));
        }
    }
}

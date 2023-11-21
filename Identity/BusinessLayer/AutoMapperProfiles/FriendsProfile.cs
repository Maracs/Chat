using AutoMapper;
using BusinessLayer.DTOs;
using DataAccessLayer.Entities;


namespace BusinessLayer.AutoMapperProfiles
{
    public class FriendsProfile:Profile
    {
        public FriendsProfile()
        {
            CreateMap<Friend, FullUserInfoDto>()
           .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.UserFriend.UserInfo.FirstName))
           .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.UserFriend.UserInfo.LastName))
           .ForMember(dest => dest.UserNickName, opt => opt.MapFrom(src => src.UserFriend.UserInfo.Nickname))
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserFriendId))
           .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserFriend.Role.Name))
           .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.UserFriend.UserInfo.Phone))
           .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.UserFriend.AccountName))
           .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => src.UserFriend.UserInfo.Info));
        }
    }
}

using AutoMapper;
using BusinessLayer.DTOs;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Extentions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
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

            CreateMap<Blocked, BlockingDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.BlockedUserId))
                .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => src.BlockedUser.UserInfo.Info))
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.BlockedUser.AccountName))
                .ForMember(dest => dest.UserNickName, opt => opt.MapFrom(src => src.BlockedUser.UserInfo.Nickname));

        }
    }
}

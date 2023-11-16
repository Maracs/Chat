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

            CreateMap<User, FullUserInfoDto>()
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.UserInfo.FirstName))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.UserInfo.LastName))
                .ForMember(dest => dest.UserNickName, opt => opt.MapFrom(src => src.UserInfo.Nickname))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.UserInfo.Phone))
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.AccountName))
                .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => src.UserInfo.Info));

            CreateMap<Blocked, BlockingDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.BlockedUserId))
                .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => src.BlockedUser.UserInfo.Info))
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.BlockedUser.AccountName))
                .ForMember(dest => dest.UserNickName, opt => opt.MapFrom(src => src.BlockedUser.UserInfo.Nickname));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.UserInfo.Nickname))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserInfo.Phone))
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.AccountName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}

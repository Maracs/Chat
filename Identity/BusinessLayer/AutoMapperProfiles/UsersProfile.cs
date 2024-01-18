using AutoMapper;
using BusinessLayer.AutoMapperProfiles.Resolvers;
using BusinessLayer.DTOs;
using DataAccessLayer.Entities;
using Grpc.DTOs;

namespace BusinessLayer.AutoMapperProfiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, FullUserInfoDto>()
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.UserInfo.FirstName))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.UserInfo.LastName))
                .ForMember(dest => dest.UserNickName, opt => opt.MapFrom(src => src.UserInfo.Nickname))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.UserInfo.Phone))
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.AccountName))
                .ForMember(dest => dest.UserInfo, opt => opt.MapFrom(src => src.UserInfo.Info))
                .ForMember(dest => dest.IsOnline, opt => opt.MapFrom<UserOnlineResolver>());

            CreateMap<User, UserDto>()
              .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.UserInfo.Nickname))
              .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserInfo.Phone))
              .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.AccountName))
              .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
              .ForMember(dest => dest.IsOnline, opt => opt.MapFrom<UserOnlineResolver>());

            CreateMap<FullUserInfoWithoutIdDto, UserInfo>();

            CreateMap<User, UserNicknameDto>()
                .ForMember(dest => dest.NickName, opt => opt.MapFrom(src => src.UserInfo.Nickname));
        }
    }
}

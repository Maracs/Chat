using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Shared;



namespace Application.AutoMapperProfiles
{
    public class GroupsProfile : Profile
    {
        public GroupsProfile()
        {
            CreateMap<Group, GroupDto>();
            CreateMap<GroupUser, UserGroupDto>();
            CreateMap<CreateGroupDto, Group>();
            CreateMap<Group, GroupWithUserNicknameDto>();
            CreateMap<UserNicknameDto, GroupWithUserNicknameDto>()
                .ForMember(dest => dest.CreatorNickName, opt => opt.MapFrom(src => src.NickName))
                .ForMember(dest => dest.CreatorAccountName, opt => opt.MapFrom(src => src.AccountName));
        }
    }
}

using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using System;


namespace Application.AutoMapperProfiles
{
    public class GroupsProfile : Profile
    {
        public GroupsProfile()
        {
            CreateMap<Group, GroupDto>();
            CreateMap<GroupUser, UserGroupDto>();
            CreateMap<CreateGroupDto, Group>();
        }
    }
}

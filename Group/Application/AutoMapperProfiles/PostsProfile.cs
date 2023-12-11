using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapperProfiles
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<PostDto, Post>().ForMember(x => x.Photos, opt => opt.Ignore());
        }
    }
}

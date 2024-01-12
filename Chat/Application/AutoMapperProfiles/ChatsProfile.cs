using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Shared;

namespace Application.AutoMapperProfiles
{
    public class ChatsProfile : Profile
    {
        public ChatsProfile()
        {
            CreateMap<Chat, ChatDto>();
            CreateMap<ChatUser, UserChatDto>();
            CreateMap<CreateChatDto, Chat>();
            CreateMap<Chat, ChatWithUserNicknameDto>();
            CreateMap<UserNicknameDto, ChatWithUserNicknameDto>()
                .ForMember(dest => dest.CreatorNickName, opt => opt.MapFrom(src => src.NickName))
                .ForMember(dest => dest.CreatorAccountName, opt => opt.MapFrom(src => src.AccountName));
        }
    }
}

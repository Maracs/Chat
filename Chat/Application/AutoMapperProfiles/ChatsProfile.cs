using Application.Dtos;
using AutoMapper;
using Domain.Entities;


namespace Application.AutoMapperProfiles
{
    public class ChatsProfile : Profile
    {
        public ChatsProfile()
        {
            CreateMap<Chat, ChatDto>();

            CreateMap<ChatUser, UserChatDto>();
        }
    }
}

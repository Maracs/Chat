using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using System;


namespace Application.AutoMapperProfiles
{
    public class ChatsProfile : Profile
    {
        public ChatsProfile()
        {
            CreateMap<Chat, ChatDto>();
            CreateMap<ChatUser, UserChatDto>();
            CreateMap<CreateChatDto, Chat>();
        }
    }
}

using Application.Dtos;
using AutoMapper;
using Domain.Entities;


namespace Application.AutoMapperProfiles
{
    public class MessageProfile:Profile
    {
        public MessageProfile()
        {
            CreateMap<ChatMessage, MessageDto>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Message.Content))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.MessageStatus.Status))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MessageId));
            CreateMap<MessageDto, Message>();
            CreateMap<MessageDto, ChatMessage>();
        }
    }
}

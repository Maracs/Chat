using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

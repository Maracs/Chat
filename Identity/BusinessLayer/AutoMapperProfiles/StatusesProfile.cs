using AutoMapper;
using BusinessLayer.DTOs;
using DataAccessLayer.Entities;


namespace BusinessLayer.AutoMapperProfiles
{
    public class StatusesProfile:Profile
    {
        public StatusesProfile()
        {
            CreateMap<Status, StatusDto>();
        }
    }
}

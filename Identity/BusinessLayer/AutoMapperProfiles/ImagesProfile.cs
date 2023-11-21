using AutoMapper;
using BusinessLayer.DTOs;
using DataAccessLayer.Entities;


namespace BusinessLayer.AutoMapperProfiles
{
    public class ImagesProfile:Profile
    {
        public ImagesProfile()
        {
            CreateMap<Image, ImageDto>();
        }
    }
}

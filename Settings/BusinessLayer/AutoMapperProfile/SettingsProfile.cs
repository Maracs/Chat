using AutoMapper;
using BusinessLayer.Dtos;
using DataAccessLayer.Entities;

namespace BusinessLayer.AutoMapperProfile
{
    public class SettingsProfile: Profile
    {
        public SettingsProfile()
        {
            CreateMap<SettingsInfo, SettingDto>();
            CreateMap<SettingDto, SettingsInfo>();
        }
    }
}

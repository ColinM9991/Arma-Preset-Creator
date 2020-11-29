using ArmaPresetCreator.Web.Models;
using ArmaPresetCreator.Web.Models.DTO;
using AutoMapper;

namespace ArmaPresetCreator.Web
{
    public class AutoMapperConfigurationCreator
    {
        public static MapperConfiguration CreateMappingConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SteamPublishedFileDetailDto, SteamWorkshopItem>()
                    .ForMember(dest => dest.FileType, memberOptions => memberOptions.MapFrom(src => src.FileType))
                    .ForMember(dest => dest.Flags, memberOptions => memberOptions.MapFrom(src => src.Flags))
                    .ForMember(dest => dest.PublishedFileId, memberOptions => memberOptions.MapFrom(src => src.PublishedFileId))
                    .ForMember(dest => dest.Name, memberOptions => memberOptions.MapFrom(src => src.Title));

                cfg.CreateMap<SteamWorkshopItem, ArmaAddon>()
                    .ForMember(dest => dest.Name, memberOptions => memberOptions.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Url, memberOptions => memberOptions.MapFrom(src => src.Url));

                cfg.CreateMap<SteamWorkshopItem, ArmaPresetRequest>()
                    .ForMember(dest => dest.Name, memberOptions => memberOptions.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Items, memberOptions => memberOptions.Ignore());
            });
        }
    }
}

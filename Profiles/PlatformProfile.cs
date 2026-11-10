using AutoMapper;

namespace PlatformService.Profiles
{
    public class PlatformProfile:Profile
    {

        public PlatformProfile()
        {
            // source to -> target 

            CreateMap<Models.Platform, Dtos.PlatformReadDto>();

            CreateMap<Dtos.PlatformRCreateDto, Models.Platform>();
            
        }
    }
}
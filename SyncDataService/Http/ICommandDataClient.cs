using PlatformService.Dtos;

namespace PlatformService.SyncdataService.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}
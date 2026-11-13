using PlatformService.Dtos;

namespace PlatformService.SyncdataService.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public HttpCommandDataClient(HttpClient client, IConfiguration configuration)
        {
            _httpClient = client;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var httpContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(plat), System.Text.Encoding.UTF8, "Application/json");
            var response =  await _httpClient.PostAsync($"{_configuration["CommandService"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("++ POST sync to CommandService Successful");
            }else
            {
                Console.WriteLine("-- Error, POST sync to CommandService Failed");
            }
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using platformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncdataService.Http;

namespace PlatformService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _dataClient;

        public PlatformsController(IPlatformRepo repo, IMapper mapper, ICommandDataClient dataClient)
        {
            _repo = repo;
            _mapper = mapper;
            _dataClient = dataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("getting Platforms for you   <3");
            var retPlatformItem = _repo.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(retPlatformItem));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Console.WriteLine("searching Platforms for you   <3");
            var retPlatformItem = _repo.GetPlatformById(id);
            if (retPlatformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(retPlatformItem));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformRCreateDto platformCreateDto)
        {

            platformCreateDto.Description = platformCreateDto.Description ?? "none"; //doppletMoppelt
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();
            var retPlatformDto = _mapper.Map<PlatformReadDto>(platformModel);

            try
            {
                await _dataClient.SendPlatformToCommand(retPlatformDto);
            }catch (Exception ex)
            {
                Console.WriteLine($"could not send data to CommandServiced: {ex.Message}");
                
            }
            return CreatedAtRoute( nameof(GetPlatformById), new {id = retPlatformDto.Id}, retPlatformDto);
            
        }
    }

    
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using platformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repo;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformRCreateDto platformCreateDto)
        {

            platformCreateDto.Description = platformCreateDto.Description ?? "none"; //doppletMoppelt
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repo.CreatePlatform(platformModel);
            _repo.SaveChanges();

            var retPlatformDto = _mapper.Map<PlatformReadDto>(platformModel);
            return CreatedAtRoute( nameof(GetPlatformById), new {id = retPlatformDto.Id}, retPlatformDto);
            
        }
    }

    
}
using System.ComponentModel.DataAnnotations;

namespace PlatformService.Dtos
{
    public class PlatformRCreateDto
    {

        // public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Cost { get; set; }


        public string? Description { get; set; } = "none";
        
    }
}
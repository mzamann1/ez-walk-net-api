using System.ComponentModel.DataAnnotations;

namespace EZWalk.API.DTOs.Region
{
    public class AddRegionRequestDTO 
    {
        [Required]
        [StringLength(200)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string? ImageUrl { get; set; }
    }
}

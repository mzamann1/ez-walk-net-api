using System.ComponentModel.DataAnnotations;

namespace EZWalk.API.DTOs.Region
{
    public class RegionDTO
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string ImageUrl { get; set; }
    }
}

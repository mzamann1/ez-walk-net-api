using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EZWalk.API.DTOs.Region;
using EZWalk.API.DTOs.Difficulty;

namespace EZWalk.API.DTOs.Walk
{
    public class WalkDto
    {

        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public double LengthInKm { get; set; }

        [Required]
        [StringLength(200)]
        public string? WalkImageUrl { get; set; }

        public DifficultyDto Difficulty { get; set; }
        public RegionDTO Region { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EZWalk.API.DTOs.Walk
{
    public class UpdateWalkRequestDto
    {

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]

        public string Description { get; set; }

        [Required]

        public double LengthInKm { get; set; }


        [StringLength(200)]
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}

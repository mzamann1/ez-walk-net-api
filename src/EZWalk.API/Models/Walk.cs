using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EZWalk.API.Models
{
    public class Walk
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public double LengthInKm { get; set; }

        [StringLength(200)] public string? WalkImageUrl { get; set; } = null;

        [ForeignKey("Difficulty")]
        public Guid DifficultyId { get; set; }

        [ForeignKey("Region")]
        public Guid RegionId { get; set; }


        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }






    }
}
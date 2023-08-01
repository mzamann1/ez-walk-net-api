
using System.ComponentModel.DataAnnotations;

namespace EZWalk.API.DTOs.Difficulty
{
    public class AddDifficultyRequestDto
    {
        [Required]
        [StringLength(100)]

        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EZWalk.API.DTOs.Difficulty
{
    public class UpdateDifficultyRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EZWalk.API.DTOs.Difficulty;

public class DifficultyDto
{
    [Required]
    public Guid Id { get; set; }

    [Required][StringLength(100)] public string Name { get; set; }
}
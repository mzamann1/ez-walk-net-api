using System.ComponentModel.DataAnnotations;

namespace EZWalk.API.DTOs.Image;

public class ImageUploadRequestDto
{
    [Required]
    public IFormFile File { get; set; }

    [Required]
    [StringLength(100)]
    public string FileName { get; set; }


    [StringLength(500)]
    public string? FileDescription { get; set; }
}
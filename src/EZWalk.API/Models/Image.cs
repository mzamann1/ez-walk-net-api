using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EZWalk.API.Models;


[Table("Images")]
public class Image
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    public string FileName { get; set; }
    [StringLength(500)]

    public string? FileDescription { get; set; }

    [StringLength(10)]
    public string Extension { get; set; }
    public long FileSize { get; set; }

    [StringLength(150)]

    public string FilePath { get; set; }

    [NotMapped]
    public IFormFile File { get; set; }
}
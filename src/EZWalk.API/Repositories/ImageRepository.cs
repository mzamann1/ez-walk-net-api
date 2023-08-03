using EZWalk.API.Context;
using EZWalk.API.Models;

namespace EZWalk.API.Repositories;

class ImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly WalkDbContext _walkDbContext;

    public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, WalkDbContext walkDbContext)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor ?? throw new  ArgumentNullException(nameof(httpContextAccessor));
        _walkDbContext = walkDbContext;
    }
    public async Task<Image> Upload(Image image)
    {
        var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.Extension}");

        await using var stream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(stream);

        var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.Extension}";

        image.FilePath = urlFilePath;

        await _walkDbContext.Images.AddAsync(image);
        await _walkDbContext.SaveChangesAsync();

        return image;
    }
}
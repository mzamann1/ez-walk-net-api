using EZWalk.API.Models;

namespace EZWalk.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

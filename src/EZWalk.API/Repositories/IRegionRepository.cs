using EZWalk.API.DTOs.Region;
using EZWalk.API.Models;

namespace EZWalk.API.Repositories
{
    public interface IRegionRepository
    {
        Task<Region> UpdateAsync(Guid id, Region region);
        Task<Region> AddAsync(Region  region);
        Task DeleteAsync(Guid id);
        Task<Region> GetByIdAsync(Guid id);
        Task<List<Region>> GetAllAsync();
    }
}

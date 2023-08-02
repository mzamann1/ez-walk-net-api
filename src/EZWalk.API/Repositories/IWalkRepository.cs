using EZWalk.API.Models;

namespace EZWalk.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 25);
        Task<Walk?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<Walk> UpdateAsync(Guid id, Walk walk);


    }
}

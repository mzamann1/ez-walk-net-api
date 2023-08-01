using EZWalk.API.Models;

namespace EZWalk.API.Repositories
{
    public interface IDifficultyRepository
    {
        Task<Difficulty> UpdateAsync(Guid id, Difficulty difficulty);
        Task<Difficulty> AddAsync(Difficulty difficulty);
        Task DeleteAsync(Guid id);
        Task<Difficulty> GetByIdAsync(Guid id);
        Task<List<Difficulty>> GetAllAsync();
    }
}

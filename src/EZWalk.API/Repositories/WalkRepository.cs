using EZWalk.API.Context;
using EZWalk.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq;
using Region = EZWalk.API.Models.Region;

namespace EZWalk.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WalkDbContext _dbContext;

        public WalkRepository(WalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 25)
        {
            var walks = _dbContext.Walks.Include(w => w.Difficulty).Include(w => w.Region).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Name == filterQuery);
                }
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Description == filterQuery);
                }
            }

            walks = walks.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending.HasValue && isAscending.Value ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
                }
                else if (sortBy.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending.HasValue && isAscending.Value ? walks.OrderBy(w => w.Description) : walks.OrderByDescending(w => w.Description);
                }

            }

            return await walks.ToListAsync();

        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks.Include(w => w.Difficulty).Include(w => w.Region)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var walk = await _dbContext.Walks.FindAsync(id);

            if (walk == null) throw new KeyNotFoundException();

            _dbContext.Walks.Remove(walk);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var walkSearched = await _dbContext.Walks.FindAsync(id);

            if (walkSearched == null) throw new KeyNotFoundException(nameof(Guid));

            walkSearched.Name = walk.Name;
            walkSearched.Description = walk.Description;
            walkSearched.LengthInKm = walk.LengthInKm;
            walkSearched.DifficultyId = walk.DifficultyId;
            walkSearched.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();
            return walkSearched;
        }
    }
}

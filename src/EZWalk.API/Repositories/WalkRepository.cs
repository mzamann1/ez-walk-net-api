using EZWalk.API.Context;
using EZWalk.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
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

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _dbContext.Walks.Include(w => w.Difficulty).Include(w => w.Region).ToListAsync();
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

using EZWalk.API.Context;
using EZWalk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EZWalk.API.Repositories;

public class DifficultyRepository : IDifficultyRepository
{
    private readonly WalkDbContext _dbContext;

    public DifficultyRepository(WalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Difficulty> UpdateAsync(Guid id, Difficulty difficulty)
    {
        var difficultySearched = await _dbContext.Difficulties.FindAsync(id);

        if (difficultySearched == null) throw new KeyNotFoundException(nameof(Guid));

        difficultySearched.Name = difficulty.Name;

        await _dbContext.SaveChangesAsync();
        return difficultySearched;
    }

    public async Task<Difficulty> AddAsync(Difficulty difficulty)
    {
        await _dbContext.Difficulties.AddAsync(difficulty);
        await _dbContext.SaveChangesAsync();
        return difficulty;
    }

    public async Task DeleteAsync(Guid id)
    {
        var difficulty = await _dbContext.Difficulties.FindAsync(id);

        if (difficulty == null) throw new KeyNotFoundException();

        _dbContext.Difficulties.Remove(difficulty);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Difficulty> GetByIdAsync(Guid id)
    {
        return await _dbContext.Difficulties.FindAsync(id);
    }

    public async Task<List<Difficulty>> GetAllAsync()
    {
        return await _dbContext.Difficulties.ToListAsync();
    }
}
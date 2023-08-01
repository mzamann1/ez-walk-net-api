using AutoMapper;
using EZWalk.API.Context;
using EZWalk.API.DTOs.Region;
using EZWalk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EZWalk.API.Repositories;

public class RegionRepository : IRegionRepository
{
    private readonly WalkDbContext _dbContext;

    public RegionRepository(WalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Region> UpdateAsync(Guid id, Region region)
    {
        var regionSearched = await _dbContext.Regions.FindAsync(id);

        if (regionSearched == null) throw new KeyNotFoundException(nameof(Guid));

        regionSearched.Code = region.Code;
        regionSearched.Name = region.Name;
        regionSearched.ImageUrl = region.ImageUrl;

        await _dbContext.SaveChangesAsync();
        return regionSearched;
    }

    public async Task<Region> AddAsync(Region region)
    {
        _dbContext.Regions.Add(region);
        await _dbContext.SaveChangesAsync();
        return region;

    }

    public async Task DeleteAsync(Guid id)
    {
        var region = await _dbContext.Regions.FindAsync(id);

        if (region == null) throw new KeyNotFoundException();

        _dbContext.Regions.Remove(region);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Region> GetByIdAsync(Guid id)
    {
        return await _dbContext.Regions.FindAsync(id);
    }

    public async Task<List<Region>> GetAllAsync()
    {
        return await _dbContext.Regions.ToListAsync();
    }
}
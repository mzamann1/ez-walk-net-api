using AutoMapper;
using EZWalk.API.Context;
using EZWalk.API.DTOs.Region;
using EZWalk.API.Models;
using EZWalk.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EZWalk.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;

    public RegionsController(IRegionRepository regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    // GET: api/Regions
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RegionDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RegionDTO>>> GetRegions()
    {
        var list = await _regionRepository.GetAllAsync();
        return Ok(_mapper.Map<List<RegionDTO>>(list));
    }

    // GET: api/Regions/5
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(RegionDTO), StatusCodes.Status200OK)]

    public async Task<ActionResult<Region>> GetById(Guid id)
    {
        var region = await _regionRepository.GetByIdAsync(id);

        if (region == null) return NotFound();

        return Ok(_mapper.Map<RegionDTO>(region));
    }

    // PUT: api/Regions/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]

    public async Task<IActionResult> PutRegion([FromQuery] Guid id, [FromBody] UpdateRegionRequestDTO region)
    {
        try
        {
            var model = _mapper.Map<Region>(region);
            model = await _regionRepository.UpdateAsync(id, model);
            return Ok(_mapper.Map<RegionDTO>(model));

        }
        catch (Exception ex)
        {
            if (ex is KeyNotFoundException)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(ex.Message);
            }
        }


    }

    // POST: api/Regions
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(typeof(RegionDTO), StatusCodes.Status201Created)]
    public async Task<ActionResult<Region>> CreateRegion([FromBody] AddRegionRequestDTO region)
    {
        var model = _mapper.Map<Region>(region);

        var createdModel = await _regionRepository.AddAsync(model);

        var regionDto = _mapper.Map<RegionDTO>(createdModel);

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    // DELETE: api/Regions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegion(Guid id)
    {
        try
        {
            await _regionRepository.DeleteAsync(id);
            return NoContent();

        }
        catch (Exception ex)
        {
            if (ex is KeyNotFoundException)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
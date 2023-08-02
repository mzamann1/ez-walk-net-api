using AutoMapper;
using EZWalk.API.Context;
using EZWalk.API.DTOs.Region;
using EZWalk.API.Models;
using EZWalk.API.Repositories;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Reader,Admin")]
    public async Task<ActionResult<IEnumerable<RegionDTO>>> GetRegions()
    {
        var list = await _regionRepository.GetAllAsync();
        return Ok(_mapper.Map<List<RegionDTO>>(list));
    }

    // GET: api/Regions/5
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(RegionDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "Reader,Admin")]

    public async Task<ActionResult<Region>> GetById(Guid id)
    {
        var region = await _regionRepository.GetByIdAsync(id);

        if (region == null) return NotFound();

        return Ok(_mapper.Map<RegionDTO>(region));
    }

    // PUT: api/Regions/5
    [HttpPut]
    [Authorize(Roles = "Writer,Admin")]
    [ProducesResponseType(typeof(RegionDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
            return BadRequest(ex.Message);
        }
    }

    // POST: api/Regions
    [HttpPost]
    [Authorize(Roles = "Writer,Admin")]
    [ProducesResponseType(typeof(RegionDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Region>> CreateRegion([FromBody] AddRegionRequestDTO region)
    {
        var model = _mapper.Map<Region>(region);

        var createdModel = await _regionRepository.AddAsync(model);

        var regionDto = _mapper.Map<RegionDTO>(createdModel);

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    // DELETE: api/Regions/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Writer,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> DeleteRegion(Guid id)
    {
        try
        {
            await _regionRepository.DeleteAsync(id);
            return NoContent();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
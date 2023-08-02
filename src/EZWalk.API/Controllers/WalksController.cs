using AutoMapper;
using EZWalk.API.DTOs.Walk;
using EZWalk.API.Models;
using EZWalk.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EZWalk.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _walkRepository;

    public WalksController(IWalkRepository walkRepository, IMapper mapper)
    {
        _walkRepository = walkRepository;
        _mapper = mapper;
    }

    // GET: api/Walks
    [HttpGet]
    [Authorize(Roles = "Reader,Admin")]
    [ProducesResponseType(typeof(IEnumerable<WalkDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<WalkDto>>> GetWalks([FromQuery] string? filterOn,
        [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
        [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var list = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
        return Ok(_mapper.Map<List<WalkDto>>(list));
    }

    // GET: api/Walks/5
    [HttpGet("{id:Guid}")]
    [Authorize(Roles = "Reader,Admin")]
    [ProducesResponseType(typeof(WalkDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WalkDto>> GetWalk(Guid id)
    {
        var walk = await _walkRepository.GetByIdAsync(id);

        if (walk == null) return NotFound();

        return Ok(_mapper.Map<WalkDto>(walk));
    }

    // PUT: api/Walks/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:Guid}")]
    [Authorize(Roles = "Writer,Admin")]
    [ProducesResponseType(typeof(WalkDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutWalk(Guid id, UpdateWalkRequestDto walk)
    {
        try
        {
            var model = _mapper.Map<Walk>(walk);
            model = await _walkRepository.UpdateAsync(id, model);
            return Ok(_mapper.Map<WalkDto>(model));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/Walks
    [HttpPost]
    [Authorize(Roles = "Writer,Admin")]
    [ProducesResponseType(typeof(WalkDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WalkDto>> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
    {
        var model = _mapper.Map<Walk>(addWalkRequestDto);
        await _walkRepository.CreateAsync(model);

        var walkDto = _mapper.Map<WalkDto>(model);
        return CreatedAtAction("GetWalk", new { id = walkDto.Id }, walkDto);
    }

    // DELETE: api/Walks/5
    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Writer,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteWalk(Guid id)
    {
        try
        {
            await _walkRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
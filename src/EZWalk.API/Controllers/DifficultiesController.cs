using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EZWalk.API.Context;
using EZWalk.API.Models;
using EZWalk.API.Repositories;
using AutoMapper;
using EZWalk.API.DTOs.Difficulty;
using EZWalk.API.DTOs.Walk;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly IMapper _mapper;

        public DifficultiesController(IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            _difficultyRepository = difficultyRepository;
            _mapper = mapper;
        }

        // GET: api/Difficulties
        [HttpGet]
        [Authorize(Roles = "Reader,Admin")]
        [ProducesResponseType(typeof(IEnumerable<DifficultyDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DifficultyDto>>> GetDifficulties()
        {
            var list = await _difficultyRepository.GetAllAsync();
            return Ok(_mapper.Map<List<DifficultyDto>>(list));
        }

        // GET: api/Difficulties/5
        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader,Admin")]
        [ProducesResponseType(typeof(DifficultyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DifficultyDto>> GetDifficulty(Guid id)
        {
            var difficulty = await _difficultyRepository.GetByIdAsync(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DifficultyDto>(difficulty));
        }

        // PUT: api/Difficulties/5
        [HttpPut("{id:Guid}")]
        [Authorize(Roles = "Writer,Admin")]
        [ProducesResponseType(typeof(DifficultyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDifficulty(Guid id, UpdateDifficultyRequestDto difficulty)
        {
            try
            {
                var model = _mapper.Map<Difficulty>(difficulty);
                model = await _difficultyRepository.UpdateAsync(id, model);
                return Ok(_mapper.Map<DifficultyDto>(model));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Difficulties
        [HttpPost]
        [Authorize(Roles = "Writer,Admin")]
        [ProducesResponseType(typeof(DifficultyDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<DifficultyDto>> PostDifficulty(AddDifficultyRequestDto addDifficultyRequestDto)
        {
            var model = _mapper.Map<Difficulty>(addDifficultyRequestDto);
            await _difficultyRepository.AddAsync(model);

            var difficultyDto = _mapper.Map<DifficultyDto>(model);
            return CreatedAtAction("GetDifficulty", new { id = difficultyDto.Id }, difficultyDto);
        }

        // DELETE: api/Difficulties/5
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer,Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteDifficulty(Guid id)
        {
            try
            {
                await _difficultyRepository.DeleteAsync(id);
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EZWalk.API.Context;
using EZWalk.API.DTOs.Walk;
using EZWalk.API.Models;
using EZWalk.API.Repositories;
using EZWalk.API.DTOs.Region;
using System.Drawing;

namespace EZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {

            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        // GET: api/Walks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Walk>>> GetWalks()
        {
            var list = await _walkRepository.GetAllAsync();
            return Ok(_mapper.Map<List<WalkDto>>(list));
        }

        // GET: api/Walks/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<WalkDto>> GetWalk(Guid id)
        {
            var walk = await _walkRepository.GetByIdAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walk));
        }

        // PUT: api/Walks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:Guid}")]
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

        // POST: api/Walks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WalkDto>> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var model = _mapper.Map<Walk>(addWalkRequestDto);
            await _walkRepository.CreateAsync(model);

            var walkDto = _mapper.Map<WalkDto>(model);
            return CreatedAtAction("GetWalk", new { id = walkDto.Id }, walkDto);
        }

        // DELETE: api/Walks/5
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            try
            {
                await _walkRepository.DeleteAsync(id);
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
}
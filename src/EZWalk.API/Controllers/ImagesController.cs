using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EZWalk.API.Context;
using EZWalk.API.DTOs.Image;
using EZWalk.API.Models;
using EZWalk.API.Repositories;

namespace EZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }



        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult<Image>> PostImage([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {

            ValidateFileUpload(imageUploadRequestDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = new Image
            {
                File = imageUploadRequestDto.File,
                Extension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                FileSize = imageUploadRequestDto.File.Length,
                FileName = imageUploadRequestDto.FileName,
                FileDescription = imageUploadRequestDto.FileDescription
            };

            await _imageRepository.Upload(image);

            return Ok(image);
        }


        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("File", "Unsupported file extension");
            }

            if (request.File.Length > 10485760) //10MB
            {
                ModelState.AddModelError("File", "File size is mote than 10MB");
            }
        }
    }
}

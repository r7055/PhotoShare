﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoShare.Core.DTOs;
using PhotoShare.Service.Services;

namespace PhotoShare.Api.Controllers
{
    [ApiController]
    [Route("api/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly PhotoService _photoService;

        public PhotoController(PhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto([FromBody] PhotoDto photoDto)
        {
            var createdPhoto = await _photoService.UploadPhoto(photoDto);
            return CreatedAtAction(nameof(GetPhotoById), new { id = createdPhoto.Id }, createdPhoto);
        }

        [HttpGet("{albumId}")]
        public async Task<IActionResult> GetPhotosByAlbumId(int albumId)
        {
            var photos = await _photoService.GetPhotosByAlbumId(albumId);
            return Ok(photos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            await _photoService.DeletePhoto(id);
            return NoContent();
        }
    }

}

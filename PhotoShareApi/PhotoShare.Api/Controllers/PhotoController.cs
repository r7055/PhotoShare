using Microsoft.AspNetCore.Http;
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

        [HttpGet("photo/{id}")]
        public async Task<IActionResult> GetPhotoById(int id)
        {
            var photo = await _photoService.GetByIdAsync(id);
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            await _photoService.DeleteAsync(id);
            return NoContent();
        }
    }

}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoShare.Api.PostModels;
using PhotoShare.Core.DTOs;
using PhotoShare.Core.IServices;
using PhotoShare.Service.Services;

namespace PhotoShare.Api.Controllers
{
    [ApiController]
    [Route("api/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public PhotoController(IPhotoService photoService,IMapper mapper)
        {
            _photoService = photoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhoto([FromBody] PhotoPostModel photoPostModel)
        {
            var photoDto = _mapper.Map<PhotoDto>(photoPostModel);
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

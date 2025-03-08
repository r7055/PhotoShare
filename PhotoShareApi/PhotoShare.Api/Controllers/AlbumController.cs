using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoShare.Core.DTOs;
using PhotoShare.Service.Services;

namespace PhotoShare.Api.Controllers
{
    [ApiController]
    [Route("api/albums")]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService _albumService;

        public AlbumController(AlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromBody] AlbumDto albumDto)
        {
            var createdAlbum = await _albumService.CreateAlbum(albumDto);
            return CreatedAtAction(nameof(GetAlbumById), new { id = createdAlbum.Id }, createdAlbum);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums()
        {
            var albums = await _albumService.GetAllAlbums();
            return Ok(albums);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            await _albumService.DeleteAlbum(id);
            return NoContent();
        }
    }

}

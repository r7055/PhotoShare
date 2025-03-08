using PhotoShare.Core.DTOs;
using PhotoShare.Core.IRepositories;
using PhotoShare.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Service.Services
{
    public class AlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<AlbumDto> CreateAlbum(AlbumDto albumDto)
        {
            var album = new Album
            {
                Title = albumDto.Title,
                Description = albumDto.Description,
                CreatedAt = DateTime.Now
            };
            await _albumRepository.AddAsync(album);
            return albumDto;
        }

        public async Task<IEnumerable<AlbumDto>> GetAllAlbums()
        {
            var albums = await _albumRepository.GetAllAsync();
            return albums.Select(a => new AlbumDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description
            });
        }

        public async Task DeleteAlbum(int id)
        {
            await _albumRepository.DeleteAsync(id);
        }
    }

}

using PhotoShare.Core.DTOs;
using PhotoShare.Core.IRepositories;
using PhotoShare.Core.IServices;
using PhotoShare.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Service.Services
{
    public class AlbumService:IAlbumService
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
        public async Task<IEnumerable<Album>> GetAllAsync()
        {
            return await _albumRepository.GetAllAsync();
        }

        public async Task<Album> GetByIdAsync(int id)
        {
            return await _albumRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Album album)
        {
            await _albumRepository.AddAsync(album);
        }

        public async Task UpdateAsync(Album album)
        {
            await _albumRepository.UpdateAsync(album);
        }

        public async Task DeleteAsync(int id)
        {
            await _albumRepository.DeleteAsync(id);
        }

    }

}

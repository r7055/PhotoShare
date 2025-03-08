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
    public class PhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task<PhotoDto> UploadPhoto(PhotoDto photoDto)
        {
            var photo = new Photo
            {
                Url = photoDto.Url,
                UploadedAt = DateTime.Now
            };
            await _photoRepository.AddAsync(photo);
            return photoDto; // החזרת ה-DTO שנוצר
        }

        public async Task<IEnumerable<PhotoDto>> GetPhotosByAlbumId(int albumId)
        {
            var photos = await _photoRepository.GetAllAsync();
            return photos.Where(p => p.Albums.Any(a => a.Id == albumId)).Select(p => new PhotoDto
            {
                Id = p.Id,
                Url = p.Url
            });
        }

        public async Task DeletePhoto(int id)
        {
            await _photoRepository.DeleteAsync(id);
        }
    }

}

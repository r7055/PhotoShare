using AutoMapper;
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
    public class PhotoService:IPhotoService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PhotoService(IRepositoryManager repositoryManager,IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        //public async Task<PhotoDto> UploadPhoto(PhotoDto photoDto)
        //{
        //    var photo=_mapper.Map<Photo>(photoDto);
        //   var res= await _repositoryManager.Photo.AddAsync(photo);
        //    return _mapper.Map<PhotoDto>(res);
        //}

        public async Task<IEnumerable<PhotoDto>> GetPhotosByAlbumId(int albumId)
        {
            var photos = await _repositoryManager.Photo.GetAllAsync();
            return photos.Where(p => p.Albums.Any(a => a.Id == albumId)).Select(p => new PhotoDto
            {
                Id = p.Id,
                Url = p.Url
            });
        }

        public async Task<IEnumerable<PhotoDto>> GetAllAsync()
        {
            var photos = await _repositoryManager.Photo.GetAllAsync();
            return _mapper.Map<IEnumerable<PhotoDto>>(photos);
        }

        public async Task<PhotoDto> GetByIdAsync(int id)
        {
            var photo = await _repositoryManager.Photo.GetByIdAsync(id);
            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task CreateAsync(PhotoDto photoDto)
        {
            var photo= _mapper.Map<Photo>(photoDto);
            await _repositoryManager.Photo.AddAsync(photo);
        }

        public async Task UpdateAsync(PhotoDto photoDto)
        {
            var photo = _mapper.Map<Photo>(photoDto);
            await _repositoryManager.Photo.UpdateAsync(photo);
        }

        public async Task DeleteAsync(int id)
        {
            await _repositoryManager.Photo.DeleteAsync(id);
        }

    }

}

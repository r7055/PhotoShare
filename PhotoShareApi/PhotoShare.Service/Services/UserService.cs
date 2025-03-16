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
    public class UserService:IUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UserService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
             var res = await _repositoryManager.User.GetAllAsync();
            return _mapper.Map < IEnumerable <UserDto>>(res);
        }
     
        public async Task<UserDto> GetByIdAsync(int id)
        {
            var res = await _repositoryManager.User.GetByIdAsync(id);
            return _mapper.Map<UserDto>(res);

        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var res = await _repositoryManager.User.AddAsync(user);
            return _mapper.Map<UserDto>(res);
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _repositoryManager.User.UpdateAsync(user);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _repositoryManager.User.GetByIdAsync(id);
            if (user != null)
            {
                // Delete all photos associated with the user
                //var photos = await _photoRepository.GetAllAsync();
                var photos = await _repositoryManager.Photo.GetAllAsync();
                foreach (var photo in photos.Where(p => p.UserId == id))
                {
                    await _repositoryManager.Photo.DeleteAsync(photo.Id);
                }

                // Delete all albums associated with the user
                var albums = await _repositoryManager.Album.GetAllAsync();
                foreach (var album in albums.Where(a => a.UserId == id))
                {
                    await _repositoryManager.Album.DeleteAsync(album.Id);
                }

                // Delete the user
                await _repositoryManager.User.DeleteAsync(id);
                await _repositoryManager.SaveAsync();
            }
        }

    }

}

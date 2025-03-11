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
        private readonly IUserRepository _userRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IAlbumRepository _albumRepository;

        public UserService(IUserRepository userRepository, IPhotoRepository photoRepository, IAlbumRepository albumRepository)
        {
            _userRepository = userRepository;
            _photoRepository = photoRepository;
            _albumRepository = albumRepository;
        }

        public async Task<UserDto> RegisterUser(UserDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                CreatedAt = DateTime.Now
            };
            await _userRepository.AddAsync(user);
            return userDto; // החזרת ה-DTO שנוצר
        }

        //public async Task<IEnumerable<UserDto>> GetAllUsers()
        //{
        //    var users = await _userRepository.GetAllAsync();
        //    return users.Select(u => new UserDto
        //    {
        //        Id = u.Id,
        //        FirstName = u.FirstName,
        //        LastName = u.LastName,
        //        Email = u.Email
        //    });
        //}

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                // Delete all photos associated with the user
                var photos = await _photoRepository.GetAllAsync();
                foreach (var photo in photos.Where(p => p.UserId == id))
                {
                    await _photoRepository.DeleteAsync(photo.Id);
                }

                // Delete all albums associated with the user
                var albums = await _albumRepository.GetAllAsync();
                foreach (var album in albums.Where(a => a.UserId == id))
                {
                    await _albumRepository.DeleteAsync(album.Id);
                }

                // Delete the user
                await _userRepository.DeleteAsync(id);
            }
        }

    }

}

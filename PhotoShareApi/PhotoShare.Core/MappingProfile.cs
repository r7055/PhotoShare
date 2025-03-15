using AutoMapper;
using PhotoShare.Core.DTOs;
using PhotoShare.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserDto>()
           .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<UserDto, User>()
                     .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => HashPassword(src.Password))); // כאן תוכל לקרוא לפונקציה שמבצעת את הה hashing
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
        }
        private string HashPassword(string password)
        {
            Console.WriteLine("HashPassword: " + password);
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password)); // דוגמה פשוטה
        }
    }
}

﻿using AutoMapper;
using PhotoShare.Core.DTOs;
using PhotoShare.Core.Models;

namespace PhotoShare.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
           .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<UserDto, User>()
                     .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password)); // כאן תוכל לקרוא לפונקציה שמבצעת את הה hashing
            CreateMap<Album, AlbumDto>().ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Tag, TagDto>().ReverseMap();
        }
    }
}

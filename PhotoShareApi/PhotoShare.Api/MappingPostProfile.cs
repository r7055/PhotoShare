using AutoMapper;
using PhotoShare.Api.PostModels;
using PhotoShare.Core.DTOs;

namespace PhotoShare.Api
{
    public class MappingPostProfile : Profile
    {
        public MappingPostProfile()
        {
            CreateMap<PhotoPostModel, PhotoDto>();
            CreateMap<AlbumPostModel, AlbumDto>();
            CreateMap<TagPostModel, TagDto>();
            CreateMap<UserPostModel, UserDto>();
        }
    }
}

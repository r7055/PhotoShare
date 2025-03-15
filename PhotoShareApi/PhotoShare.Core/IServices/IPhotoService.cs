﻿using PhotoShare.Core.DTOs;
using PhotoShare.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Core.IServices
{
    public interface IPhotoService : IService<PhotoDto>
    {
        Task<IEnumerable<PhotoDto>> GetPhotosByAlbumId(int albumId);
        Task UploadPhoto(PhotoDto photoDto);
    }
}

//using Microsoft.EntityFrameworkCore;//??
using Microsoft.EntityFrameworkCore;
using PhotoShare.Core.DTOs;
using PhotoShare.Core.IRepositories;
using PhotoShare.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Data.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(PhotoShareContext context) : base(context)
        {
        }
        public async Task<Album> GetAlbumIncludePhotosAsync(int albumId)
        {
            return await _dbSet.Include(a => a.Photos).Where(a => a.Id == albumId).FirstOrDefaultAsync();

        }
    }

}

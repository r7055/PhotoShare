using PhotoShare.Core.IRepositories;
using PhotoShare.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Data.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly PhotoShareContext _context;

        public PhotoRepository(PhotoShareContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            return await _context.Photos.ToListAsync();
        }

        public async Task<Photo> GetByIdAsync(int id)
        {
            return await _context.Photos.FindAsync(id);
        }

        public async Task AddAsync(Photo entity)
        {
            await _context.Photos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Photo entity)
        {
            _context.Photos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var photo = await GetByIdAsync(id);
            if (photo != null)
            {
                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync();
            }
        }
    }

}

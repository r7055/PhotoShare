//using Microsoft.EntityFrameworkCore;//??
using PhotoShare.Core.IRepositories;
using PhotoShare.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly PhotoShareContext _context;

        public AlbumRepository(PhotoShareContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Album>> GetAllAsync()
        {
            return await _context.Albums.ToListAsync();
        }

        public async Task<Album> GetByIdAsync(int id)
        {
            return await _context.Albums.FindAsync(id);
        }

        public async Task AddAsync(Album entity)
        {
            await _context.Albums.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Album entity)
        {
            _context.Albums.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var album = await GetByIdAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
                await _context.SaveChangesAsync();
            }
        }
    }

}

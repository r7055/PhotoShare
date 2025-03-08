using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Core.IServices
{
    public interface IPhotoService
    {
        Task<IEnumerable<Photo>> GetAllPhotosAsync();
        Task<Photo> GetPhotoByIdAsync(int id);
        Task CreatePhotoAsync(Photo photo);
        Task UpdatePhotoAsync(Photo photo);
        Task DeletePhotoAsync(int id);
    }
}

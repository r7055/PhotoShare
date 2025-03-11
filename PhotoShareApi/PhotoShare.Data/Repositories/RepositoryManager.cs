using PhotoShare.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Data.Repositories
{
    internal class RepositoryManager : IRepositoryManager
    {
        PhotoShareContext _photoShareContext;

        public IAlbumRepository Album {  get; }

        public IPhotoRepository Photo {  get; }

        public IUserRepository User {  get; }

        public ITagRepository Tag {  get; }

        RepositoryManager(PhotoShareContext photoShareContext, IAlbumRepository album, IPhotoRepository photo, IUserRepository user, ITagRepository tag)
        {
            _photoShareContext = photoShareContext;
            Album = album;
            Photo = photo;
            User = user;
            Tag = tag;
        }
        public async Task SaveAsync()
        {
            await _photoShareContext.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PhotoShare.Core.Models; // Add this using directive

namespace PhotoShare.Data
{
    public class PhotoShareContext : DbContext
    {
        public PhotoShareContext(DbContextOptions<PhotoShareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}

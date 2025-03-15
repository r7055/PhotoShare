using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Core.Models;
using static Mysqlx.Error.Types;
namespace PhotoShare.Data;

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

}
   

    
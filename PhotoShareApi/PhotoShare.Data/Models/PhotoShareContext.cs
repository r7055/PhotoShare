using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PhotoShare.Data.Models;

public partial class PhotoShareContext : DbContext
{
    public PhotoShareContext()
    {
    }

    public PhotoShareContext(DbContextOptions<PhotoShareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=photo_share_db;user=root;password=aA1795aA");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("albums");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoverImageId).HasColumnName("cover_image_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Albums)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("albums_ibfk_1");

            entity.HasMany(d => d.Photos).WithMany(p => p.Albums)
                .UsingEntity<Dictionary<string, object>>(
                    "AlbumPhoto",
                    r => r.HasOne<Photo>().WithMany()
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("album_photos_ibfk_2"),
                    l => l.HasOne<Album>().WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("album_photos_ibfk_1"),
                    j =>
                    {
                        j.HasKey("AlbumId", "PhotoId").HasName("PRIMARY");
                        j.ToTable("album_photos");
                        j.HasIndex(new[] { "PhotoId" }, "photo_id");
                        j.IndexerProperty<int>("AlbumId").HasColumnName("album_id");
                        j.IndexerProperty<int>("PhotoId").HasColumnName("photo_id");
                    });
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("photos");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("uploaded_at");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Photos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("photos_ibfk_1");

            entity.HasMany(d => d.Tags).WithMany(p => p.Files)
                .UsingEntity<Dictionary<string, object>>(
                    "FileTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("file_tags_ibfk_2"),
                    l => l.HasOne<Photo>().WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("file_tags_ibfk_1"),
                    j =>
                    {
                        j.HasKey("FileId", "TagId").HasName("PRIMARY");
                        j.ToTable("file_tags");
                        j.HasIndex(new[] { "TagId" }, "tag_id");
                        j.IndexerProperty<int>("FileId").HasColumnName("file_id");
                        j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tags");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValueSql("'user'")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

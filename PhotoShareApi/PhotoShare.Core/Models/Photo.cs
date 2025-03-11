using System;
using System.Collections.Generic;

namespace PhotoShare.Core.Models;

public partial class Photo
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Url { get; set; } = null!;

    public DateTime? UploadedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}

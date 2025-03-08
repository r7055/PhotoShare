using System;
using System.Collections.Generic;

namespace PhotoShare.Data.Models;

public partial class Album
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? CoverImageId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}

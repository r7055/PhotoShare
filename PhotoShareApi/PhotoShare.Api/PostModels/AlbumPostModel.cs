namespace PhotoShare.Api.PostModels
{
    public class AlbumPostModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string BackgroundColor { get; set; } = null!;
        public string? Tags { get; set; }
        public string? User { get; set; }
    }
}

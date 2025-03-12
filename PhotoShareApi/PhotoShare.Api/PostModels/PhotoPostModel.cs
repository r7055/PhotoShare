namespace PhotoShare.Api.PostModels
{
    public class PhotoPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string Caption { get; set; }
        public string? Album { get; set; }
        public string? Tags { get; set; }

    }
}

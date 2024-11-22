namespace APIBasic.DTOs
{
    public class UpdateFileRequest
    {
        public int CurrentVersion { get; set; }
        public string ResourceType { get; set; } = null!;

        public string ResourceName { get; set; } = null!;

        public string FileContent { get; set; } = null!;

        public string? Remarks { get; set; }

        public string? Thumbnail1 { get; set; }

        public string? Thumbnail2 { get; set; }
    }
}

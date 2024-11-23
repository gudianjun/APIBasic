using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class UpdateFileRequest
    {
        [Required]
        public int CurrentVersion { get; set; }

        [StringLength(50)]
        public string ResourceName { get; set; } = null!;

        public string? FileContent { get; set; } = null!;

        public string? Remarks { get; set; }

        public string? Thumbnail1 { get; set; }

        public string? Thumbnail2 { get; set; }
    }
}

using APIBasic.Enums;
using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class CreateFileRequest
    {
        [Required]
        [RegularExpression(@$"({ResourceTypeName.Plan}|{ResourceTypeName.D3D})")]
        public string ResourceType { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string ResourceName { get; set; } = null!;

        public string FileContent { get; set; } = null!;

        public string? Remarks { get; set; }

        public string? Thumbnail1 { get; set; }

        public string? Thumbnail2 { get; set; }
    }
}

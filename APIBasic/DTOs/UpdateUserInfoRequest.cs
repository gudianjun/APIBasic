using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class UpdateUserInfoRequest
    {
        [StringLength(250)]
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        public string? CompanyName { get; set; }
        [StringLength(50)]
        public string? AvatarIcon { get; set; }

        [StringLength(50)]
        public string? Tel { get; set; }
    }
}

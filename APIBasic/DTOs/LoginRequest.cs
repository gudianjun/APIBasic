using APIBasic.Enums;
using APIBasic.Validations;
using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class LoginRequest
    {
        [StringLength(50)]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [StringLength(50)]
        [Required(ErrorMessage = "Username is required")] 
        public string Password { get; set; } = null!;

        [RegularExpression(@"(mobile|browser)")]
        public string AudienceName { get; set; } = Audience.Mobile; 
    }
}

using APIBasic.Enums;
using APIBasic.Validations;
using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class LoginRequest
    {
        [CustomValidation(typeof(BaseValidator), "ValidateName")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Username is required")] 
        public string Password { get; set; } = null!;

        [RegularExpression(@"(mobile|browser)")]
        public string AudienceName { get; set; } = Audience.Mobile; 
    }
}

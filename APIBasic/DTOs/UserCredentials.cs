using APIBasic.Enums;
using APIBasic.Validations;
using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class UserCredentials : IValidatableObject
    {
        [CustomValidation(typeof(BaseValidator), "ValidateName")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Username is required")]
        [MailValidation(ErrorMessage = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public string Password { get; set; } = null!;

        [RegularExpression(@"(0|1)")]
        public int AudienceType { get; set; } = (int)AudienceEnum.Web;
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Username == "111")
            {
                yield return new ValidationResult(
                    $"Classic movies must have a release year no later than {Username}.",
                    new[] { nameof(Username) });
            }
        }
    }
}

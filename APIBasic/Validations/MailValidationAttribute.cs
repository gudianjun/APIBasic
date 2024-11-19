using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace APIBasic.Validations
{
    public class MailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Email address is required.");
            }

            var email = value.ToString();
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email!))
            {
                return new ValidationResult("Invalid email address format.");
            }

            return ValidationResult.Success;
        }// 静态方法来验证邮箱地址
        public static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
    } 
}

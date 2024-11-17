using System.ComponentModel.DataAnnotations;

namespace APIBasic.Validations
{
    public class MailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //return new ValidationResult(string.IsNullOrEmpty(this.ErrorMessage)? "Mail is already taken." : this.ErrorMessage);
            return ValidationResult.Success;
        }
    }
    
}

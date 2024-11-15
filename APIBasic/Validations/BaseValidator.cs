using APIBasic.Enums;
using System.ComponentModel.DataAnnotations;

namespace APIBasic.Validations
{
    public class BaseValidator
    {
        public static ValidationResult? ValidateName(string name, ValidationContext context)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new ValidationResult("Name cannot be empty.");
            }
            return ValidationResult.Success;
        }
        public static ValidationResult? ValidateAudience(AudienceEnum name, ValidationContext context)
        { 
            return ValidationResult.Success;
        }
    }
}

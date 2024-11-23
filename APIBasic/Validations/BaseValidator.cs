using APIBasic.Enums;
using System.ComponentModel.DataAnnotations;

namespace APIBasic.Validations
{
    /// <summary>
    /// 简单校验类型
    /// </summary>
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
        public static ValidationResult? ValidateAudience(Audience name, ValidationContext context)
        {
            return ValidationResult.Success;
        }
    }
}

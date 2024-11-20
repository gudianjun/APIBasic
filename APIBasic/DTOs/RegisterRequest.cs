using APIBasic.Validations;
using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Name is required")] 
        [StringLength(50)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MailValidation(ErrorMessage = "Not a valid email address")]
        [StringLength(50)]
        public string? MailAddress { get; set; } 
        //
        // 摘要:
        //     The code sent to the user's email to reset the password. To get the reset code,
        //     first make a "/forgotPassword" request.
        [Required(ErrorMessage = "ResetCode is required")] 
        [StringLength(5)]
        [RegularExpression(@"\d{5}")]
        public required string ResetCode { get; init; }

 
        [StringLength(50)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage = "Password must contain at least 8 characters, including uppercase, lowercase letters and numbers")]
        [Required(ErrorMessage = "Password is required")]
         public required string Password { get; init; }
        [StringLength(250)]
        public string? CompanyName { get; set; }
        [StringLength(250)]
        public string? Address { get; set; }

        /// <summary>
        /// 日本地址邮编格式 七位数字
        /// </summary>
        [StringLength(7)]
        [RegularExpression(@"\d{7}")]
        public string? Zip { get; set; }
    }
}

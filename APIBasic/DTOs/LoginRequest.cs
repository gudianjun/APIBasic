using APIBasic.Enums;
using APIBasic.Validations;
using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class LoginRequest 
    {
        /// <summary>
        /// 用户名。如果是邮件地址，就是邮件地址。
        /// </summary>
        [StringLength(50)] 
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [StringLength(50)]
        [Required(ErrorMessage = "Password is required")] 
        public string Password { get; set; } = null!;

        [RegularExpression(@"(mobile|browser)")]
        public string AudienceName { get; set; } = Audience.Mobile;
 
    }
}

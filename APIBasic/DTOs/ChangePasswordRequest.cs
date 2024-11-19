using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class ChangePasswordRequest
    {
        /// <summary>
        /// 旧的密码
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "OldPassword is required")]
        public string OldPassword { get; set; } = null!;
        /// <summary>
        /// 新的密码
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassword { get; set; } = null!;
    }
}

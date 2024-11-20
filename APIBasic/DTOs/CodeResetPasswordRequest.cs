using APIBasic.Validations;
using System.ComponentModel.DataAnnotations;

namespace APIBasic.DTOs
{
    public class CodeResetPasswordRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [MailValidation(ErrorMessage = "Not a valid email address")]
        [StringLength(50)]
        public required string Email { get; init; }
        //
        // 摘要:
        //     The code sent to the user's email to reset the password. To get the reset code,
        //     first make a "/forgotPassword" request.
        [Required(ErrorMessage = "ResetCode is required")]

        [StringLength(5)]
        public required string ResetCode { get; init; }
        //
        // 摘要:
        //     The new password the user with the given Microsoft.AspNetCore.Identity.Data.ResetPasswordRequest.Email
        //     should login with. This will replace the previous password.
        [Required(ErrorMessage = "NewPassword is required")]

        [StringLength(50)]
        public required string NewPassword { get; init; }

    }
}

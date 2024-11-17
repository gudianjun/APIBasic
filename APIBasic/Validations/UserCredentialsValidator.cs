using APIBasic.DTOs;
using FluentValidation;
using FluentValidation.Results;

namespace APIBasic.Validations
{
    /// <summary>
    /// 流畅验证
    /// </summary>
    public class UserCredentialsValidator : AbstractValidator<UserCredentials>
    {
        public UserCredentialsValidator()
        {
        
             
        }
        
    }
}

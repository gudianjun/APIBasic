using APIBasic.Controllers;
using APIBasic.DTOs;
using APIBasic.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Caching.Memory;

namespace APIBasic.Validations
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        private readonly ITopWindowService _topWindowService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RegisterRequestValidator> _logger;
        private readonly IMemoryCache _memoryCache;
        public RegisterRequestValidator(IConfiguration configuration, ITopWindowService topWindowService
            , ILogger<RegisterRequestValidator> logger, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _topWindowService = topWindowService;
            _configuration = configuration;

            // 邮件地址也不存在。
            // 当mailAddress不为空时，验证mailAddress是否存在 
            RuleFor(x => x.MailAddress).Must((x, cancellation) =>
            {
                bool has = _topWindowService.CheckMailExist(x.MailAddress).GetAwaiter().GetResult();
                return !has;
            }).WithMessage("MailAddress already exists"); 
        }
    }
}

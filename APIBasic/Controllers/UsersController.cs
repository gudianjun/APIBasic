using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Enums;
using APIBasic.Models;
using APIBasic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Win32;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;

namespace APIBasic.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly ITopWindowService _topWindowService;
        private readonly IConfiguration _configuration; 
        private readonly ILogger<UsersController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;  
        public UsersController(
            IConfiguration configuration 
            , ITopWindowService topWindowService
            , ILogger<UsersController> logger
            , IMemoryCache memoryCache
            , IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
            _logger = logger;
            _topWindowService = topWindowService;
            _configuration = configuration; 
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
        {
            var response = await _topWindowService.RegisterAsync(request);
            // 实现用户信息修改逻辑
            return response;
        }

        [HttpGet] 
        public async Task<ActionResult<GetUserInfoResponse>> GetUserInfo()
        {
            // 通过HttpContext.User.Identity 获得当前用户的ID信息 
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                string userId = claimsIdentity.FindFirst(KeyName.USER_ID)?.Value 
                    ?? throw new ArgumentNullException(nameof(userId), "User ID cannot be null");
                var response = await _topWindowService.GetUserInfoAsync(userId);
                return (new ApiResponse<GetUserInfoResponse>(response)).Result();
            }
            throw new NotImplementedException(); 
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut] 
        public async Task<ActionResult<UpdateUserInfoResponse>> UpdateUserInfo([FromBody]  UpdateUserInfoRequest request)
        {
            var response = await _topWindowService.UpdateUserInfoAsync(request);
            // 实现用户信息修改逻辑
            return response;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("password")] 
        public async Task<ActionResult<ChangePasswordResponse>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var resopnse = await _topWindowService.ChangePasswordAsync(request);
            // 实现修改密码逻辑
            return resopnse;
        }

        /// <summary>
        /// 发送找回密码验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("send-reset-password-code")]
        [AllowAnonymous]
        public async Task<ActionResult<SendResetPasswordCodeResponse>> SendResetPasswordCode([FromBody] SendResetPasswordCodeRequest request)
        {
            var response = await _topWindowService.SendResetPasswordCodeAsync(request);
            return response;
        }

        /// <summary>
        /// 验证码修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<ActionResult<CodeResetPasswordResponse>> CodeResetPassword([FromBody] CodeResetPasswordRequest request)
        {       
            var response = await _topWindowService.CodeResetPasswordAsync(request);
            return response;
        }

        [HttpPost("send-code")]
        [AllowAnonymous]
        public async Task<ActionResult<SendCodeResponse>> SendCode([FromBody] SendCodeRequest request)
        {
            var response = await _topWindowService.SendCodeAsync(request);
            return response;
        }
    }
}

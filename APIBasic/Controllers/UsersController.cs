using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Models;
using APIBasic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Win32;

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
        public UsersController(IConfiguration configuration , ITopWindowService topWindowService
            , ILogger<UsersController> logger, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _topWindowService = topWindowService;
            _configuration = configuration; 
        }

        [HttpPost]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
        {
            // 实现注册逻辑
            return (new ApiResponse<RegisterResponse>(null)).Result();
        }

        [HttpGet] 
        public async Task<ActionResult<GetUserInfoResponse>> GetUserInfo()
        {
            // 实现用户信息检索逻辑
            return (new ApiResponse<GetUserInfoResponse>(null)).Result(); 
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
    }
}

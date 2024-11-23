using APIBasic.DTOs;
using APIBasic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace APIBasic.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITopWindowService _topWindowService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IMemoryCache _memoryCache;
        public AuthController(IConfiguration configuration, ITopWindowService topWindowService
            , ILogger<AuthController> logger, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _topWindowService = topWindowService;
            _configuration = configuration;
        }
        /// <summary>
        /// 登录。 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        /// Token：访问token，所有的api请求使用该token
        /// RefreshToken：刷新token，当访问token过期时，使用refresh token来获取新的token
        /// </returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var response = await _topWindowService.LoginAsync(request);
            return response.Result();
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<string>> Logout()
        {
            await _topWindowService.LogoutAsync();
            return (new ApiResponse<string>("Logout Successful")).Result();
        }
        /// <summary>
        /// Refresh token
        /// 当访问token过期时，使用refresh token来获取新的token
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh")]
        [Authorize]
        public async Task<ActionResult<LoginResponse>> Refresh()
        {
            var response = await _topWindowService.RefreshAsync();
            return response.Result();
        }
    }
}

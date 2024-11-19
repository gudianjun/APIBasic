
 
using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Enums;
using APIBasic.Services.Interfaces;
using APIBasic.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace APIBasic.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITopWindowService  _topWindowService;
        private readonly IConfiguration _configuration; 
        private readonly ILogger<AuthController> _logger;
        private readonly IMemoryCache _memoryCache; 
        public AuthController(IConfiguration configuration , ITopWindowService topWindowService
            , ILogger<AuthController> logger, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _topWindowService = topWindowService;
            _configuration = configuration; 
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var response = await _topWindowService.LoginAsync(request);
            return response;
        }

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
            return response;
        }
    } 
}

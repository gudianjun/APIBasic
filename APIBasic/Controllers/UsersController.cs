using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Models;
using APIBasic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // 实现注册逻辑
            return (new ApiResponse<RegisterResponse>(null)).Result();
        }

        [HttpGet] 
        public async Task<IActionResult> GetUserInfo()
        {
            // 实现用户信息检索逻辑
            return (new ApiResponse<GetUserInfoResponse>(null)).Result(); 
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo([FromBody]  UpdateUserInfoRequest request)
        {
            // 实现用户信息修改逻辑
            return (new ApiResponse<UpdateUserInfoResponse>(null)).Result();
        }

        [HttpPut("password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            // 实现修改密码逻辑
            return (new ApiResponse<ChangePasswordResponse>(null)).Result();
        }
    }
}

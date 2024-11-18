using APIBasic.Controllers;
using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Enums;
using APIBasic.Models;
using APIBasic.Repositories.Interfaces;
using APIBasic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace APIBasic.Services.Implementations
{
    public class TopWindowService : ITopWindowService
    {
        private readonly ITopWindowRepository  _topWindowRepository;
        private readonly IConfiguration _configuration; 
        private readonly ILogger<TopWindowService> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TopWindowService(IConfiguration configuration,  
            ITopWindowRepository topWindowRepository
            , ILogger<TopWindowService> logger, IMemoryCache memoryCache,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
            _logger = logger;
            _topWindowRepository = topWindowRepository;
            _configuration = configuration; 
        }
  

        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var user = await _topWindowRepository.GetUserInfoForUserNameAsync(request.Username);
            if (user == null)
            {
                var response = new ApiResponse<string>(HttpStatusCode.NotFound, "Incorrect username or password.");
                return response.Result();
            }
            else
            {
                // 检查密码是否正确
                if (user.Password != request.Password)
                {
                    var response = new ApiResponse<string>(HttpStatusCode.NotFound, "Incorrect username or password.");
                    return response.Result();
                }
                else
                {
                    string session = Guid.NewGuid().ToString();
                    var claims = new[]
                    {
                        new Claim(KeyName.SESSION_ID, session),
                        new Claim(KeyName.USER_ID, user.UserId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, Roles.User)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: request.AudienceName,
                        claims: claims,
                        expires: DateTime.Now.AddDays(30),
                        signingCredentials: creds);
                    string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    // 保存用户Token到内存中，用来登录校验
                    await _topWindowRepository.SaveLoginInfoAsync((int)user.UserId, request.AudienceName, session);
                    var response = new ApiResponse<object>(new { token = tokenString });
                    return response.Result();
                }
            }
        }
        
        public async Task LogoutAsync()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            if(claimsIdentity != null)
            {
                var userId = claimsIdentity.FindFirst(KeyName.USER_ID)?.Value;
                var aud = claimsIdentity.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud)?.Value;
                if (userId != null && aud!=null)
                {
                    await _topWindowRepository.SaveLoginInfoAsync(int.Parse(userId), aud, "");
                }
            }
            throw new NotImplementedException();
        }
        public async Task<ChangePasswordResponse> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetDesignDetailsResponse> GetDesignDetailsAsync(int designId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetDesignsResponse> GetDesignsAsync(GetDesignsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetTypeDesignsResponse> GetTypeDesignsAsync(GetTypeDesignsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserInfoResponse> GetUserInfoAsync(string userId)
        {
            throw new NotImplementedException();
        }

       

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateUserInfoResponse> UpdateUserInfoAsync(UpdateUserInfoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

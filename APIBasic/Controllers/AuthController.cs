
 
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
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // var albums = _context.Albums!.Select(a => a.Title).ToList();
            if (true)
            {
                var claims = new[]
                {
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
                var response = new ApiResponse<object>(new { token = tokenString });

                return response.Result();
            }

            return (new ApiResponse<string>((int)HttpStatusCode.Unauthorized, null)).Result();
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return (new ApiResponse<string>("Logout Successful")).Result();
        } 
    } 
}

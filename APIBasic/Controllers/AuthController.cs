
using APIBasic.Common;
using APIBasic.Enums;
using APIBasic.Models;
using APIBasic.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace APIBasic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlDbContext _context;
        public AuthController(IConfiguration configuration, MySqlDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public IActionResult GenerateToken([FromBody] UserCredentials credentials)
        {
            var albums = _context.Albums!.Select(a => a.Title).ToList();
            if (true)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, credentials.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, Roles.User)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: Enum.GetName(typeof(AudienceEnum), credentials.AudienceType)!.ToLower(),
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: creds);
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                var response = new ApiResponse<object>(new { token = tokenString });

                return response.Result();
            }

            return (new ApiResponse<string>((int)HttpStatusCode.Unauthorized, null )).Result();
        }
    }

    public class UserCredentials : IValidatableObject
    {
        [CustomValidation(typeof(BaseValidator), "ValidateName")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Username is required")]
        [MailValidation(ErrorMessage ="aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public string Password { get; set; } = null!;

        [RegularExpression(@"(0|1)")]
        public int AudienceType { get; set; } = (int)AudienceEnum.Web;
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Username == "111")
            {
                yield return new ValidationResult(
                    $"Classic movies must have a release year no later than { Username }.",
                    new[] { nameof(Username) });
            }
        }
    }
}

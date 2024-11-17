using APIBasic.Configurations;
using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIBasic.Controllers
{
    /// <summary>
    /// 测试接口类
    /// 1，默认两个用户，管理员，user
    /// 2，用户可以登录，并返回token
    /// 3，两个接口，一个是管理员专用，一个是用户专用
    /// 3，上传文件，下载文件。
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly APIConfig _apiConfig;
        private readonly MySqlDbContext _dbContext;
        public SampleController(IConfiguration configuration, IOptionsMonitor<APIConfig> apiConfig, MySqlDbContext context)
        {
            _dbContext = context;
            _apiConfig = apiConfig.CurrentValue;
            _configuration = configuration;
        }

        private static readonly List<UserCredentials> Users = new()
        {
            new UserCredentials { Username = "admin", Password = "111", AudienceName = Audience.Browser },
            new UserCredentials { Username = "user", Password = "111", AudienceName = Audience.Mobile }
        };

        /// <summary>
        /// 测试登录接口，默认使用admin，和user，密码都是111
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            var user = Users.FirstOrDefault(u => u.Username == credentials.Username && u.Password == credentials.Password);
            if (user == null)
            {
                return (new ApiResponse<string>(System.Net.HttpStatusCode.Unauthorized, "", null)).Result();
            }
            string rolename = Roles.User;
            if (user.Username == "admin")
            {
                rolename = Roles.Admin;
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, rolename)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
 

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: user.AudienceName,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds);
            return (new ApiResponse<object>(new { token = new JwtSecurityTokenHandler().WriteToken(token) })).Result();
        }
        /// <summary>
        /// 只有使用了admin的token才能访问
        /// </summary>
        /// <returns></returns>
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndpoint()
        {
            return (new ApiResponse<string>("This is an admin endpoint")).Result(); 
        }
        /// <summary>
        /// 只有使用了user的token才能访问
        /// </summary>
        /// <returns></returns>
        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public IActionResult UserEndpoint()
        {
            return (new ApiResponse<string>("This is a user endpoint")).Result(); 
        }
        /// <summary>
        /// 上传单文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFile(IFormFile file)
        { 
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded" });
            }

            var filePath = Path.Combine(_apiConfig.UploadPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { message = "File uploaded successfully", filePath });
        }
        /// <summary>
        /// 上传多文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost("uploads")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return (new ApiResponse<string>(System.Net.HttpStatusCode.BadRequest, "No files uploaded")).Result();
            }

            var uploadedFiles = new List<string>();

            foreach (var file in files)
            {
                var filePath = Path.Combine(_apiConfig.UploadPath, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                uploadedFiles.Add(filePath);
            }

            return (new ApiResponse<string>("File uploaded successfully")).Result();
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("download/{fileName}")]　
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filePath = Path.Combine(_apiConfig.UploadPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { message = "File not found" });
            } 
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetPagedItems([FromQuery] PagedRequest request)
        {
            //var query = _dbContext.Items.AsQueryable();

            //// 排序
            //if (!string.IsNullOrEmpty(request.SortField))
            //{
            //    query = request.SortDirection?.ToLower() == "desc"
            //        ? query.OrderByDescending(e => EF.Property<object>(e, request.SortField))
            //        : query.OrderBy(e => EF.Property<object>(e, request.SortField));
            //}

            //// 获取总记录数
            //var totalRecords = await query.CountAsync();

            //// 分页
            //var items = await query
            //    .Skip((request.PageNumber - 1) * request.PageSize)
            //    .Take(request.PageSize)
            //    .ToListAsync();

            //// 创建分页响应
            //var response = new PagedResponse<Item>(items, request.PageNumber, request.PageSize, totalRecords);

            return Ok();
        }
    }
     
}

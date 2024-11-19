using APIBasic.Controllers;
using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Enums;
using APIBasic.Models;
using APIBasic.Repositories.Interfaces;
using APIBasic.Services.Interfaces;
using APIBasic.Utilities;
using APIBasic.Validations;
using Google.Protobuf.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace APIBasic.Services.Implementations
{
    public class TopWindowService : ITopWindowService
    {
        private readonly ITopWindowRepository _topWindowRepository;
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


        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            User? user = null;
            if (MailValidationAttribute.IsValidEmail(request.Username))
            {
                user = await _topWindowRepository.GetUserInfoForMailAddressAsync(request.Username);
            }
            else
            {
                user = await _topWindowRepository.GetUserInfoForUserNameAsync(request.Username);
            }
            if (user == null)
            {
                var response = new ApiResponse<LoginResponse>(HttpStatusCode.NotFound, "Incorrect username or password.", null);
                return response.Result();
            }
            else
            {
                // 密码验证需要加密后验证
                // 检查密码是否正确
                if (!StringHelper.VerifyPassword(request.Password, user!.Password ?? ""))
                {
                    var response = new ApiResponse<LoginResponse>(HttpStatusCode.NotFound, "Incorrect username or password.", null);
                    return response.Result();
                }
                else
                {
                    string session = Guid.NewGuid().ToString();
                    string accessToken = StringHelper.CreateToken(session, user.UserId.ToString()
                        , request.Username
                        , _configuration["Jwt:Key"]!
                        , _configuration["Jwt:Issuer"] ?? "issuer"
                        , request.AudienceName
                        , TokenType.AccessToken
                        , DateTime.Now.AddMinutes(15));
                    string refreshToken = StringHelper.CreateToken(session, user.UserId.ToString()
                       , request.Username
                       , _configuration["Jwt:Key"]!
                       , _configuration["Jwt:Issuer"] ?? "issuer"
                       , request.AudienceName
                       , TokenType.RefreshToken
                       , DateTime.Now.AddDays(30));

                    // 保存用户Token到内存中，用来登录校验
                    await _topWindowRepository.SaveLoginInfoAsync((int)user.UserId, request.AudienceName, session);
                    var response = new ApiResponse<LoginResponse>(new LoginResponse()
                    {
                        Token = accessToken,
                        RefreshToken = refreshToken,
                        userInfo = new UserInfo()
                        {
                            Address = user.Address,
                            AvatarIcon = user.AvatarIcon,
                            CompanyName = user.CompanyName,
                            Name = user.Name
                        }
                    });
                    return response.Result();
                }
            }
        }
        public async Task<ActionResult<LoginResponse>> RefreshAsync()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                string session = Guid.NewGuid().ToString();
                var userId = claimsIdentity.FindFirst(KeyName.USER_ID)?.Value;
                var userName = claimsIdentity.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                var AudienceName = claimsIdentity.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud)?.Value;

                string accessToken = StringHelper.CreateToken(session, userId!
                    , userName!
                    , _configuration["Jwt:Key"]!
                    , _configuration["Jwt:Issuer"] ?? "issuer"
                    , AudienceName!
                    , TokenType.AccessToken
                    , DateTime.Now.AddMinutes(15));
                string refreshToken = StringHelper.CreateToken(session, userId!
                   , userName!
                   , _configuration["Jwt:Key"]!
                   , _configuration["Jwt:Issuer"] ?? "issuer"
                   , AudienceName!
                   , TokenType.RefreshToken
                   , DateTime.Now.AddDays(30));

                // 保存用户Token到内存中，用来登录校验
                await _topWindowRepository.SaveLoginInfoAsync((int)int.Parse(userId!), AudienceName!,
                    session);
                var response = new ApiResponse<LoginResponse>(new LoginResponse()
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    userInfo = null
                });
                return response.Result();
            }
            return (new ApiResponse<LoginResponse>(HttpStatusCode.NotFound, "Refresh token failed, need to log in again", null)).Result();
        }
        public async Task LogoutAsync()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userId = claimsIdentity.FindFirst(KeyName.USER_ID)?.Value;
                var aud = claimsIdentity.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud)?.Value;
                if (userId != null && aud != null)
                {
                    await _topWindowRepository.SaveLoginInfoAsync(int.Parse(userId), aud, "");
                }
            }
            throw new NotImplementedException();
        }
        public async Task<ActionResult<ChangePasswordResponse>> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userId = claimsIdentity.FindFirst(KeyName.USER_ID)?.Value;
                if (userId != null)
                {
                    User? user = null;
                    if (uint.TryParse(userId, out uint id))
                    {
                        user = await _topWindowRepository.GetUserByIdAsync(id);
                    }
                    if (user != null)
                    {
                        if (!StringHelper.VerifyPassword(request.OldPassword, user.Password ?? ""))
                        {
                            return new ApiResponse<ChangePasswordResponse>(HttpStatusCode.NotFound, "Old password is incorrect", null).Result();
                        }
                        else
                        {
                            user.Password = StringHelper.HashPassword(request.NewPassword);
                            int ncount = await _topWindowRepository.UpdateUserAsync(user);
                            if (ncount > 0)
                            {
                                return new ApiResponse<ChangePasswordResponse>(null).Result();
                            }
                            else
                            {
                                return new ApiResponse<ChangePasswordResponse>(HttpStatusCode.NotFound, "Update Error!", null).Result();
                            }
                        }
                    }
                    else
                    {
                        throw new NotImplementedException("Not logged in or verification information is lost");
                    }
                }
                else
                {
                    throw new NotImplementedException("There is no user information in the token");
                }
            }
            throw new NotImplementedException("Not logged in or verification information is lost");
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

        public async Task<ActionResult<UpdateUserInfoResponse>> UpdateUserInfoAsync(UpdateUserInfoRequest request)
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity!.FindFirst(KeyName.USER_ID)?.Value;
            User? user = await _topWindowRepository.GetUserByIdAsync(uint.Parse(userId!));
            user!.Name = request.Name;
            user!.CompanyName = request.CompanyName;
            user!.Address = request.Address;
            user!.AvatarIcon = request.AvatarIcon;
            await _topWindowRepository.UpdateUserAsync(user);
            return new ApiResponse<UpdateUserInfoResponse>(null).Result();
        }
    }
}

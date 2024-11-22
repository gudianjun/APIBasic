using APIBasic.Configurations;

using APIBasic.DTOs;
using APIBasic.Enums;
using APIBasic.Models;
using APIBasic.Repositories.Interfaces;
using APIBasic.Services.Interfaces;
using APIBasic.Utilities;
using APIBasic.Validations;
using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using MimeKit;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;


namespace APIBasic.Services.Implementations
{
    public class TopWindowService : ITopWindowService
    {
        private readonly ITopWindowRepository _topWindowRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TopWindowService> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly APIConfig _apiConfig;
        private readonly IMapper _mapper;
        public TopWindowService(IConfiguration configuration,
            ITopWindowRepository topWindowRepository
            , ILogger<TopWindowService> logger
            , IMemoryCache memoryCache
            , IHttpContextAccessor httpContextAccessor
            , IOptionsMonitor<APIConfig> apiConfig
            , IMapper mapper) 
        {
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
            _logger = logger;
            _topWindowRepository = topWindowRepository;
            _configuration = configuration;
            _apiConfig = apiConfig.CurrentValue;
            _mapper = mapper;
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
                        , DateTime.Now.AddMinutes(_apiConfig.AccessTokenExpiresTime));
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
                            UserId = user.UserId,
                            Address = user.Address,
                            AvatarIcon = user.AvatarIcon,
                            CompanyName = user.CompanyName,
                            Name = user.Name!
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

      public async Task<GetUserInfoResponse> GetUserInfoAsync(string userId)
        {
            var userInfo = await _topWindowRepository.GetUserByIdAsync(uint.Parse(userId));

            if (userInfo != null)
            {
                return _mapper.Map<GetUserInfoResponse>(userInfo);
            }
            throw new KeyNotFoundException("User not found");
        }
        public async Task<ActionResult<RegisterResponse>> RegisterAsync(RegisterRequest request)
        {
            string code = _topWindowRepository.LoadResetPasswordCode("SendCode_" + request.MailAddress);
            if (code == request.ResetCode)
            {// 生成用户数据，并保存到数据库 
                User user = _mapper.Map<User>(request);
                await _topWindowRepository.NewUserAsync(user);
                return new ApiResponse<RegisterResponse>(HttpStatusCode.OK, "Successful registration", null).Result();
            }
            return new ApiResponse<RegisterResponse>(HttpStatusCode.NotFound, "Incorrect verification code", null).Result();
        }
        public async Task<ActionResult<SendCodeResponse>> SendCodeAsync([FromBody] SendCodeRequest request)
        {
            string toEmail = request.Email;
            // 生成随机5位数字验证码
            Random random = new Random();
            int code = random.Next(10000, 99999);
            _topWindowRepository.SaveResetPasswordCode("SendCode_" + toEmail, code.ToString());

            string message = $"Registration verification code is: {code}";
            string subject = "Registration verification code";
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_apiConfig.MailYourName, _apiConfig.SmtpUser));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };
            await StringHelper.SendEmailAsync(toEmail, subject, _apiConfig, emailMessage);
            return new ApiResponse<SendCodeResponse>(HttpStatusCode.OK,
            "The verification code has been sent to the specified email address", null).Result(); 
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
            user!.Tel = request.Tel;
            await _topWindowRepository.UpdateUserAsync(user);
            return new ApiResponse<UpdateUserInfoResponse>(null).Result();
        }
        // 文件相关
        public async Task<ActionResult<GetFilesResponse>> GetFilesAsync()
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            // 如果用户ID为空，返回错误
            string userId = claimsIdentity!.FindFirst(KeyName.USER_ID)?.Value 
                ?? throw new ArgumentNullException(nameof(userId), "User ID cannot be null");
            string aud = claimsIdentity.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud)?.Value 
                ?? throw new ArgumentNullException(nameof(aud), "Aud cannot be null");

            var files = await _topWindowRepository.GetDesignFilesAsync(aud, uint.Parse(userId));
            return (new ApiResponse<GetFilesResponse>(new GetFilesResponse()
            {
                DesignFiles = files
            })).Result();
        }

        public async Task<ActionResult<DownloadFileResponse>> DownloadFileAsync([Required] string fileId)
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            // 如果用户ID为空，返回错误
            string userId = claimsIdentity!.FindFirst(KeyName.USER_ID)?.Value
                ?? throw new ArgumentNullException(nameof(userId), "User ID cannot be null");
            string aud = claimsIdentity.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud)?.Value
                ?? throw new ArgumentNullException(nameof(aud), "Aud cannot be null");

            var file = await _topWindowRepository.GetDesignFileAsync(aud, uint.Parse(userId), fileId);

            // 如果文件不存在，返回资源不存在错误
            if (file == null)
            {
                return new ApiResponse<DownloadFileResponse>(HttpStatusCode.NotFound, "File not found", null).Result();
            }
            return new ApiResponse<DownloadFileResponse>(new DownloadFileResponse()
            {
                Design_File = file
            }).Result();
        }

        public async Task<ActionResult<CreateFileResponse>> CreateFileAsync([FromBody] CreateFileRequest request)
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            // 如果用户ID为空，返回错误
            string userId = claimsIdentity!.FindFirst(KeyName.USER_ID)?.Value
                ?? throw new ArgumentNullException(nameof(userId), "User ID cannot be null");
            string aud = claimsIdentity.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud)?.Value
                ?? throw new ArgumentNullException(nameof(aud), "Aud cannot be null");
            var file = _mapper.Map<DesignFile>(request);
            file.UserId = uint.Parse(userId);
            file.DeviceType = aud;
            await _topWindowRepository.AddDesignFileAsync(file);
            return new ApiResponse<CreateFileResponse>(null).Result();
        }

        public async Task<ActionResult<DeleteFileResponse>> DeleteFileAsync(string fileId)
        {
            var claimsIdentity = _httpContextAccessor?.HttpContext?.User.Identity as ClaimsIdentity;
            // 如果用户ID为空，返回错误
            string userId = claimsIdentity!.FindFirst(KeyName.USER_ID)?.Value
                ?? throw new ArgumentNullException(nameof(userId), "User ID cannot be null");
            string aud = claimsIdentity.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud)?.Value
                ?? throw new ArgumentNullException(nameof(aud), "Aud cannot be null");
            var count = await _topWindowRepository.DeleteDesignFileAsync(aud, uint.Parse(userId), fileId);
            if (count > 0)
            {
                return new ApiResponse<DeleteFileResponse>(null).Result();
            }
            return new ApiResponse<DeleteFileResponse>(HttpStatusCode.NotFound, "Delete failed", null).Result(); 
        }

        public async Task<ActionResult<UpdateFileResponse>> UpdateFileAsync([Required] string fileId, [FromBody] UpdateFileRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<SendResetPasswordCodeResponse>> SendResetPasswordCodeAsync(SendResetPasswordCodeRequest request)
        {
            var user = await _topWindowRepository.GetUserInfoForMailAddressAsync(request.Email);
            if(user != null)
            {
                string toEmail = request.Email;
                // 生成随机5位数字验证码
                Random random = new Random();
                int code = random.Next(10000, 99999);
                _topWindowRepository.SaveResetPasswordCode(toEmail, code.ToString());

                string message = $"Your password reset code is: {code}";
                string subject = "Password Reset Code";
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_apiConfig.MailYourName, _apiConfig.SmtpUser));
                emailMessage.To.Add(new MailboxAddress("", toEmail));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("plain") { Text = message };
                await StringHelper.SendEmailAsync(toEmail, subject, _apiConfig, emailMessage);
                return new ApiResponse<SendResetPasswordCodeResponse>(HttpStatusCode.OK,
                "The verification code has been sent to the specified email address", null).Result();
            }
            else
            {
                return new ApiResponse<SendResetPasswordCodeResponse>(HttpStatusCode.NotFound, "Email not found", null).Result();
            }

        }

        public async Task<ActionResult<CodeResetPasswordResponse>> CodeResetPasswordAsync(CodeResetPasswordRequest request)
        {
            string code = _topWindowRepository.LoadResetPasswordCode(request.Email);
            if(code == request.ResetCode)
            {
                var user = await _topWindowRepository.GetUserInfoForMailAddressAsync(request.Email);
                if (user != null)
                {
                    user.Password = StringHelper.HashPassword(request.NewPassword);
                    await _topWindowRepository.UpdateUserAsync(user);
                    return new ApiResponse<CodeResetPasswordResponse>(null).Result();
                }
                else
                {
                    return new ApiResponse<CodeResetPasswordResponse>(HttpStatusCode.NotFound, "User not found", null).Result();
                }
            }
            else
            {
                return new ApiResponse<CodeResetPasswordResponse>(HttpStatusCode.NotFound, "Verification code error", null).Result();
            } 
        }

        /// <summary>
        /// 检查邮箱是否存在
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckMailExist(string? mail)
        {
            if (string.IsNullOrEmpty(mail))
            {
                return true;
            }
            var has = await _topWindowRepository.CheckIfValueExistsAsync("Users", "MailAddress", mail);
            return has;
        }
    }
}

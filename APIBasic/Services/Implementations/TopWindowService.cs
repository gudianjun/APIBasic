using APIBasic.Configurations;

using APIBasic.DTOs;
using APIBasic.Enums;
using APIBasic.Models;
using APIBasic.Repositories.Interfaces;
using APIBasic.Services.Interfaces;
using APIBasic.Utilities;
using APIBasic.Validations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MimeKit;

using System.ComponentModel.DataAnnotations;
using System.Net;


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
        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
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
                return response;
            }
            else
            {
                // 密码验证需要加密后验证
                // 检查密码是否正确
                if (!StringHelper.VerifyPassword(request.Password, user!.Password ?? ""))
                {
                    var response = new ApiResponse<LoginResponse>(HttpStatusCode.NotFound, "Incorrect username or password.", null);
                    return response;
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
                    return response;
                }
            }
        }
        public async Task<ApiResponse<LoginResponse>> RefreshAsync()
        {
            var tokenInfo = HttpContextHelper.GetTokenInfo();
            string session = Guid.NewGuid().ToString();

            string accessToken = StringHelper.CreateToken(session, tokenInfo.UserId.ToString()
                , tokenInfo.UserName
                , _configuration["Jwt:Key"]!
                , _configuration["Jwt:Issuer"] ?? "issuer"
                , tokenInfo.Audience
                , TokenType.AccessToken
                , DateTime.Now.AddMinutes(15));
            string refreshToken = StringHelper.CreateToken(session, tokenInfo.UserId.ToString()
               , tokenInfo.UserName
               , _configuration["Jwt:Key"]!
               , _configuration["Jwt:Issuer"] ?? "issuer"
               , tokenInfo.Audience
               , TokenType.RefreshToken
               , DateTime.Now.AddDays(30));

            // 保存用户Token到内存中，用来登录校验
            await _topWindowRepository.SaveLoginInfoAsync((int)tokenInfo.UserId, tokenInfo.Audience,
                session);
            var response = new ApiResponse<LoginResponse>(new LoginResponse()
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                userInfo = null
            });
            return response;
        }
        public async Task LogoutAsync()
        {
            var tokenInfo = HttpContextHelper.GetTokenInfo();
            await _topWindowRepository.SaveLoginInfoAsync((int)tokenInfo.UserId, tokenInfo.Audience, "");
        }
        public async Task<ApiResponse<ChangePasswordResponse>> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var tokenInfo = HttpContextHelper.GetTokenInfo();
            User? user = null;
            user = await _topWindowRepository.GetUserByIdAsync(tokenInfo.UserId);
            if (user != null)
            {
                if (!StringHelper.VerifyPassword(request.OldPassword, user.Password ?? ""))
                {
                    return new ApiResponse<ChangePasswordResponse>(HttpStatusCode.NotFound, "Old password is incorrect", null);
                }
                else
                {
                    user.Password = StringHelper.HashPassword(request.NewPassword);
                    int nCount = await _topWindowRepository.UpdateUserAsync(user);
                    if (nCount > 0)
                    {
                        return new ApiResponse<ChangePasswordResponse>(null);
                    }
                    else
                    {
                        return new ApiResponse<ChangePasswordResponse>(HttpStatusCode.NotFound, "Update Error!", null);
                    }
                }
            }
            else
            {
                throw new NotImplementedException("Not logged in or verification information is lost");
            }
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
        public async Task<ApiResponse<RegisterResponse>> RegisterAsync(RegisterRequest request)
        {
            string code = _topWindowRepository.LoadResetPasswordCode("SendCode_" + request.MailAddress);
            if (code == request.ResetCode)
            {// 生成用户数据，并保存到数据库 
                User user = _mapper.Map<User>(request);
                await _topWindowRepository.NewUserAsync(user);
                return new ApiResponse<RegisterResponse>(HttpStatusCode.OK, "Successful registration", null);
            }
            return new ApiResponse<RegisterResponse>(HttpStatusCode.NotFound, "Incorrect verification code", null);
        }
        public async Task<ApiResponse<SendCodeResponse>> SendCodeAsync([FromBody] SendCodeRequest request)
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
            "The verification code has been sent to the specified email address", null);
        }
        public async Task<ApiResponse<UpdateUserInfoResponse>> UpdateUserInfoAsync(UpdateUserInfoRequest request)
        {
            var tokenInfo = HttpContextHelper.GetTokenInfo();
            User? user = await _topWindowRepository.GetUserByIdAsync(tokenInfo.UserId);
            user!.Name = request.Name;
            user!.CompanyName = request.CompanyName;
            user!.Address = request.Address;
            user!.AvatarIcon = request.AvatarIcon;
            user!.Tel = request.Tel;
            await _topWindowRepository.UpdateUserAsync(user);
            return new ApiResponse<UpdateUserInfoResponse>(null);
        }
        // 文件相关
        public async Task<ApiResponse<GetFilesResponse>> GetFilesAsync()
        {
            var tokenInfo = HttpContextHelper.GetTokenInfo();
            var files = await _topWindowRepository.GetDesignFilesAsync(tokenInfo.Audience, tokenInfo.UserId);
            // 得到DesignBaseInfo列表，把files转换成DesignBaseInfo列表
            var designs = files.Select(x => new DesignBaseInfo(x)).ToList();
            return (new ApiResponse<GetFilesResponse>(new GetFilesResponse()
            {
                DesignFiles = designs
            }));
        }
        public async Task<ApiResponse<DownloadFileResponse>> DownloadFileAsync([Required] string fileId)
        {
            var tokenInfo = HttpContextHelper.GetTokenInfo();
            var file = await _topWindowRepository.GetDesignFileAsync(tokenInfo.Audience, tokenInfo.UserId, fileId);

            // 如果文件不存在，返回资源不存在错误
            if (file == null)
            {
                return new ApiResponse<DownloadFileResponse>(HttpStatusCode.NotFound, "File not found", null);
            }
            return new ApiResponse<DownloadFileResponse>(new DownloadFileResponse()
            {
                Design_File = file
            });
        }
        public async Task<ApiResponse<CreateFileResponse>> CreateFileAsync([FromBody] CreateFileRequest request)
        {
            // 如果用户ID为空，返回错误
            var tokenInfo = HttpContextHelper.GetTokenInfo();
            var file = _mapper.Map<DesignFile>(request);
            file.UserId = tokenInfo!.UserId;
            file.DeviceType = tokenInfo.Audience;
            await _topWindowRepository.AddDesignFileAsync(file);
            return new ApiResponse<CreateFileResponse>(new CreateFileResponse(file));
        }
        public async Task<ApiResponse<DeleteFileResponse>> DeleteFileAsync(string fileId)
        {
            var tokenInfo = HttpContextHelper.GetTokenInfo();
            var count = await _topWindowRepository.DeleteDesignFileAsync(tokenInfo.Audience, tokenInfo.UserId, fileId);
            if (count > 0)
            {
                return new ApiResponse<DeleteFileResponse>(new DeleteFileResponse() { DeleteFileId = fileId });
            }
            return new ApiResponse<DeleteFileResponse>(HttpStatusCode.NotFound, "Delete failed", null);
        }
        public async Task<ApiResponse<UpdateFileResponse>> UpdateFileAsync([Required] string fileId, [FromBody] UpdateFileRequest request)
        {
            var tokenInfo = HttpContextHelper.GetTokenInfo();
            var file = await _topWindowRepository.GetDesignFileForIDAsync(fileId);
            if (file == null)
            {
                return new ApiResponse<UpdateFileResponse>(HttpStatusCode.NotFound, "File not found", null);
            }
            if (file.UserId != tokenInfo.UserId
                || file.DeviceType != tokenInfo.Audience)
            {
                return new ApiResponse<UpdateFileResponse>(HttpStatusCode.NotFound, "File not found", null);
            }
            if (file.CurrentVersion != request.CurrentVersion)
            {
                return new ApiResponse<UpdateFileResponse>(HttpStatusCode.NotFound, "Version mismatch", null);
            }
            file.CurrentVersion = file.CurrentVersion + 1;
            file.LastUpdatedTime = DateTime.Now;
            if (!string.IsNullOrEmpty(request.ResourceName))
            {
                file.ResourceName = request.ResourceName;
            }
            if (!string.IsNullOrEmpty(request.FileContent))
            {
                file.FileContent = request.FileContent;
            }
            if (!string.IsNullOrEmpty(request.Thumbnail1))
            {
                file.Thumbnail1 = request.Thumbnail1;
            }
            if (!string.IsNullOrEmpty(request.Thumbnail2))
            {
                file.Thumbnail2 = request.Thumbnail2;
            }
            if (!string.IsNullOrEmpty(request.Remarks))
            {
                file.Remarks = request.Remarks;
            }

            int nCount = await _topWindowRepository.UpdateDesignFileAsync(file);
            if (nCount > 0)
            {
                return new ApiResponse<UpdateFileResponse>(new UpdateFileResponse() { NewDesignFile = file });
            }
            return new ApiResponse<UpdateFileResponse>(HttpStatusCode.NotFound, "Update failed", null);
        }
        public async Task<ApiResponse<SendResetPasswordCodeResponse>> SendResetPasswordCodeAsync(SendResetPasswordCodeRequest request)
        {
            var user = await _topWindowRepository.GetUserInfoForMailAddressAsync(request.Email);
            if (user != null)
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
                "The verification code has been sent to the specified email address", null);
            }
            else
            {
                return new ApiResponse<SendResetPasswordCodeResponse>(HttpStatusCode.NotFound, "Email not found", null);
            }

        }
        public async Task<ApiResponse<CodeResetPasswordResponse>> CodeResetPasswordAsync(CodeResetPasswordRequest request)
        {
            string code = _topWindowRepository.LoadResetPasswordCode(request.Email);
            if (code == request.ResetCode)
            {
                var user = await _topWindowRepository.GetUserInfoForMailAddressAsync(request.Email);
                if (user != null)
                {
                    user.Password = StringHelper.HashPassword(request.NewPassword);
                    await _topWindowRepository.UpdateUserAsync(user);
                    return new ApiResponse<CodeResetPasswordResponse>(null);
                }
                else
                {
                    return new ApiResponse<CodeResetPasswordResponse>(HttpStatusCode.NotFound, "User not found", null);
                }
            }
            else
            {
                return new ApiResponse<CodeResetPasswordResponse>(HttpStatusCode.NotFound, "Verification code error", null);
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

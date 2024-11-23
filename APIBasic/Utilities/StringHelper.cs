using APIBasic.Configurations;
using APIBasic.DTOs;
using APIBasic.Enums;
using MailKit.Net.Smtp;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace APIBasic.Utilities
{
    public class StringHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public static string CreateToken(string session, string userId, string userName
            , string securityKey
            , string issuer
            , string audience
            , string tokenType
            , DateTime expiresTime)
        {
            var claims = new[]
                    {
                        new Claim(KeyName.TOKEN_TYPE_TITLE, tokenType),
                        new Claim(KeyName.SESSION_ID, session),
                        new Claim(KeyName.USER_ID, userId),
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, Roles.User)
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresTime,
                signingCredentials: creds);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">目标邮件地址</param> 
        /// <param name="sub">标题</param>
        /// <param name="yourName">标记发送者名</param>
        /// <param name="aPIConfig">api配置信息</param>
        /// <param name="mimeMessage">发送消息对象</param>
        /// <returns></returns>
        public static async Task SendEmailAsync(string to,
            string sub,
            APIConfig aPIConfig, MimeMessage mimeMessage)
        {
            string toEmail = to;
            string subject = sub;
            string _smtpServer = aPIConfig.SmtpServer;
            int _smtpPort = aPIConfig.SmtpPort;
            string _smtpUser = aPIConfig.SmtpUser;
            string _smtpPass = aPIConfig.SmtpPassword; // 使用应用专用密码
            var emailMessage = mimeMessage;

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpServer, _smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(_smtpUser, _smtpPass);

                await client.SendAsync(emailMessage);
                client.Disconnect(true);
            }
        }

        public static TokenInfo GetTokenInfo(ClaimsIdentity claimsIdentity)
        {
            if (claimsIdentity == null)
            {
                throw new ArgumentNullException(nameof(claimsIdentity), "ClaimsIdentity cannot be null");
            }

            string tokenType = claimsIdentity.FindFirst(KeyName.TOKEN_TYPE_TITLE)?.Value
                ?? throw new ArgumentNullException(nameof(tokenType), "Token type cannot be null");
            string sessionId = claimsIdentity.FindFirst(KeyName.SESSION_ID)?.Value
                ?? throw new ArgumentNullException(nameof(sessionId), "Session ID cannot be null");
            string userId = claimsIdentity.FindFirst(KeyName.USER_ID)?.Value
                ?? throw new ArgumentNullException(nameof(userId), "User ID cannot be null");
            string userName = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new ArgumentNullException(nameof(userName), "User name cannot be null");
            string jti = claimsIdentity.FindFirst(JwtRegisteredClaimNames.Jti)?.Value
                ?? throw new ArgumentNullException(nameof(jti), "JTI cannot be null");
            string role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value
                ?? throw new ArgumentNullException(nameof(role), "Role cannot be null");
            string audience = claimsIdentity.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud)?.Value
                ?? throw new ArgumentNullException(nameof(audience), "Audience cannot be null");
            return new TokenInfo
            {
                TokenType = tokenType,
                SessionId = sessionId,
                UserId = uint.Parse(userId),
                UserName = userName,
                Jti = jti,
                Role = role,
                Audience = audience
            };
        }
    }
}

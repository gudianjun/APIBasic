using APIBasic.Configurations;
using APIBasic.Enums;
using APIBasic.Models;
using BCrypt.Net;
using MailKit.Net.Smtp;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Ocsp;
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
        public static string CreateToken(string session , string userId, string userName
            , string securityKey
            , string issuer
            , string audience
            , string tokenType
            , DateTime expiresTime)
        {
            var claims = new[]
                    {
                        new Claim(TokenType.TOKEN_TYPE_TITLE, tokenType),
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
    }
}

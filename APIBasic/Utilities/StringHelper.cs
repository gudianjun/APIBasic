using APIBasic.Enums;
using APIBasic.Models;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
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
    }
}

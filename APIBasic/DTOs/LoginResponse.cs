using Microsoft.AspNetCore.Mvc;

namespace APIBasic.DTOs
{
    public class LoginResponse  
    {
        /// <summary>
        /// 访问用的token
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        /// 刷新token，当访问token过期时，使用refresh token来获取新的token
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// 用户信息，当refresh时，返回null，只有在登录时才返回用户信息
        /// </summary>
        public UserInfo? userInfo { get; set; }

    }
}

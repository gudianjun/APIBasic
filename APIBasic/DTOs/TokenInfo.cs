namespace APIBasic.DTOs
{
    public class TokenInfo
    {
        /// <summary>
        /// 令牌类型，访问令牌或者刷新令牌
        /// </summary>
        public string TokenType { get; set; } = null!;
        /// <summary>
        /// 根据设备类型生成的会话信息
        /// </summary>
        public string SessionId { get; set; } = null!;
        /// <summary>
        /// 用户ID
        /// </summary>
        public uint UserId { get; set; } = 0;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// 令牌唯一标识
        /// </summary>
        public string Jti { get; set; } = null!;
        /// <summary>
        /// 用户角色
        /// </summary>
        public string Role { get; set; } = null!;
        /// <summary>
        /// 设备类型信息
        /// </summary>
        public string Audience { get; set; } = null!;
    }
}

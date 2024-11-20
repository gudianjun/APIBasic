namespace APIBasic.Configurations
{
    /// <summary>
    /// API配置
    /// </summary>
    public class APIConfig
    {
        /// <summary>
        /// 伤处文件的路径
        /// </summary>
        public string UploadPath { get; set; } = null!;
        /// <summary>
        /// 访问Token过期时间，单位分钟
        /// </summary>
        public int AccessTokenExpiresTime { get; set; }

        public string MailYourName { get; set; } = null!;
        public string SmtpServer { get; set; } = null!;
        public string SmtpUser { get; set; } = null!;
        public int SmtpPort { get; set; }
        public string SmtpPassword { get; set; } = null!;
    }
}

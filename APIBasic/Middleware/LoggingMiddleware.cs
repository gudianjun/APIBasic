using Org.BouncyCastle.Asn1.Ocsp;
using System.Diagnostics;

namespace APIBasic.Middleware
{
    /// <summary>
    /// 系统日志中间件
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            HttpRequest request = context.Request;
            if (!request.Headers.TryGetValue("X-Request-ID", out var requestId))
            {
                requestId = Guid.NewGuid().ToString();
                request.Headers.TryAdd("X-Request-ID", requestId);
            }
            request.Headers["X-ClientId"] = "dev-id-1";

            var stopwatch = Stopwatch.StartNew();
            context.Response.Headers["X-Request-ID"] = requestId;
            // 获得真实的客户端IP地址
            string remoteIpAddress = context.Connection.RemoteIpAddress?.ToString()?? "unknown";
            // 获取客户端的域名
            string clientHost = request.Headers["Host"].ToString();
            string referer = request.Headers["Referer"].ToString();

            _logger.LogInformation($"Received {request.Method} RemoteIp {remoteIpAddress} request for {request.Path} Request ID: {requestId}");
            _logger.LogInformation($"Client Host: {clientHost}, Referer: {referer}");
            await _next(context);

            stopwatch.Stop();
            _logger.LogInformation($"Request ID: {requestId} Response {context.Response.StatusCode} sent after {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}

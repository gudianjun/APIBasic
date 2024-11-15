using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace APIBasic.Common
{
    //    使用标准的HTTP状态码来表示请求的结果：
    //•	200 OK：请求成功
    //•	201 Created：资源创建成功
    //•	204 No Content：请求成功但没有返回内容
    //•	400 Bad Request：请求无效
    //•	401 Unauthorized：未授权
    //•	403 Forbidden：禁止访问
    //•	404 Not Found：资源未找到
    //•	500 Internal Server Error：服务器内部错误
    public class CustomActionResult : ActionResult
    {
        private readonly object? _value;
        private readonly int _statusCode;

        public CustomActionResult(int statusCode, object? value)
        {
            _statusCode = statusCode;
            _value = value;
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_value)
            {
                ContentTypes = new MediaTypeCollection { "application/json" },
                DeclaredType = _value?.GetType(),
                StatusCode = _statusCode
            };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}

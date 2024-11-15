 
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIBasic.DTOs
{
    //    1.	成功响应：
    //•	"Success"：请求成功。
    //•	"Resource created successfully"：资源创建成功。
    //•	"Resource updated successfully"：资源更新成功。
    //•	"Resource deleted successfully"：资源删除成功。
    //2.	错误响应：
    //•	"Bad request"：请求无效。
    //•	"Unauthorized"：未授权。
    //•	"Forbidden"：禁止访问。
    //•	"Resource not found"：资源未找到。
    //•	"Internal server error"：服务器内部错误。
    //•	"Validation failed"：验证失败。
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(int statusCode, string message, T? data)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
        public ApiResponse(HttpStatusCode statusCode, string message, T? data) : this((int)statusCode, message, data)
        {
        }
        public ApiResponse(T? data)
        {
            StatusCode = (int)HttpStatusCode.OK;
            Message = "Success";
            Data = data;
        }
        public ApiResponse(HttpStatusCode statusCode, T? data) : this((int)statusCode, data)
        {

        }
        public ApiResponse(int statusCode, T? data)
        {
            string message = statusCode switch
            {
                201 => "Resource created successfully",
                204 => "Resource updated successfully",
                400 => "Bad request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Resource not found",
                500 => "Internal server error",
                _ => "Validation failed"
            };
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
        public IActionResult Result()
        {
            return new CustomActionResult(StatusCode, this);
        }
    }
}

using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using APIBasic.Middleware;
using APIBasic.Mobel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 日志记录
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// 添加内存缓存服务
builder.Services.AddMemoryCache();
// 添加JWT身份验证服务
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["Jwt:Key"]!))
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401;
            var response = new ApiResponse<string>(401, "Unauthorized", context.Exception.Message);
            return context.Response.WriteAsJsonAsync(response);
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401;
            var response = new ApiResponse<string>(401, "Unauthorized", "You are not authorized to access this resource.");
            return context.Response.WriteAsJsonAsync(response);
        }
    };
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>(); // 注册全局过滤器
});
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
}).AddApiExplorer(options=> { 
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
    options.AddApiVersionParametersWhenVersionNeutral = true;
}); // Ensure you have the following using directive 
 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication(); // 添加身份验证中间件
app.UseAuthorization();

app.MapControllers();

// 资源没有找到时
app.Use(async (context, next) =>
{
    await next();
    if (!context.Response.HasStarted)
    {
        var statusCode = context.Response.StatusCode;
        string message = statusCode switch
        {
            404 => "Resource not found",
            415 => "Unsupported Media Type",
            400 => "Bad request",
            401 => "Unauthorized",
            403 => "Forbidden",
            500 => "Internal server error",
            _ => "An error occurred"
        };

        if (statusCode >= 400)
        {
            context.Response.ContentType = "application/json";
            var response = new ApiResponse<string>(statusCode, message, null);
            await context.Response.WriteAsJsonAsync(response);
        }
    }
});

app.Run();

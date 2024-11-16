using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using APIBasic.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using APIBasic.Filters;
using APIBasic.DTOs;
using Microsoft.EntityFrameworkCore;
using APIBasic.Data;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Google.Protobuf.WellKnownTypes;
using System.Net;
using APIBasic.Configurations;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 日志记录
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// 添加内存缓存服务
builder.Services.AddMemoryCache();
// 添加DbContext配置
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MySqlDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// 配置APIConfig映射
builder.Services.Configure<APIConfig>(builder.Configuration.GetSection("APIConfig"));


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
        ValidAudiences = builder.Configuration.GetSection("Jwt:Audiences").Get<string[]>(),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["Jwt:Key"]!))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // 从请求中提取令牌
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            // 令牌验证成功后执行自定义逻辑
            var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
            //    context.Fail("Unauthorized: User does not have the required role.");
            }
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 401;
                var response = new ApiResponse<string>(401, "Unauthorized", context.Exception.Message);
                return context.Response.WriteAsJsonAsync(response);
            }
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            if (!context.Response.HasStarted)
            {
                context.HandleResponse();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 401;
                var response = new ApiResponse<string>(401, "Unauthorized",
                    context.AuthenticateFailure?.Message ?? "You are not authorized to access this resource.");
                return context.Response.WriteAsJsonAsync(response);
            }
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            if (!context.Response.HasStarted)
            {
                // 处理请求被拒绝的情况
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 403;
                var response = new ApiResponse<string>(403, "Forbidden", "You do not have permission to access this resource.");
                return context.Response.WriteAsJsonAsync(response);
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>(); // 注册全局过滤器
}).ConfigureApiBehaviorOptions(option => {
    option.InvalidModelStateResponseFactory = (context) =>
    {
        var errors = context.ModelState.Where(e => e.Value?.Errors.Count > 0)
        .Select(e => new
        {
            Field = e.Key,
            Error = e.Value?.Errors.First().ErrorMessage
        }).ToList();
        return (new ApiResponse<object>(HttpStatusCode.UnprocessableEntity, "Validation Failed!", errors)).Result();

    }; 
}) ;



builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
    options.AddApiVersionParametersWhenVersionNeutral = true;
}); // Ensure you have the following using directive 


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // 添加JWT认证支持
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}


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

// 创建上传文件夹
var apiConfig = app.Services.GetRequiredService<IOptions<APIConfig>>().Value;
if(!Directory.Exists(apiConfig.UploadPath))
{
    Directory.CreateDirectory(apiConfig.UploadPath);
}
string path = (new DirectoryInfo(apiConfig.UploadPath)).FullName;
app.Run();

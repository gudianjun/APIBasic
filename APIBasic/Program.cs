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
using FluentValidation.AspNetCore;
using FluentValidation;
using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using APIBasic.Services.Interfaces;
using APIBasic.Repositories.Interfaces;
using APIBasic.Repositories.Implementations;
using APIBasic.Services.Implementations;
using Microsoft.Extensions.Caching.Memory;
using APIBasic.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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
// 添加健康检查服务
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("Database");
// 响应压缩
builder.Services.AddResponseCompression();
// 添加复杂逻辑验证
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// 添加速率限制服务
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();
// 得到全局的上下文对象
builder.Services.AddHttpContextAccessor();
// 注册 AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// 配置 CORS 策略
var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
if(corsOrigins != null && corsOrigins.Contains("*"))
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder => builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder => builder.WithOrigins(corsOrigins!)
                              .AllowAnyMethod()
                              .AllowAnyHeader());
    });
}

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
        ClockSkew = TimeSpan.Zero,
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
            // 通过依赖注入获取IMemoryCache实例
            var memoryCache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

            // 通过token中的用户id，session信息，判断内存中保存的是否一致
            var userId = context.Principal?.FindFirstValue(KeyName.USER_ID);
            var sessionId = context.Principal?.FindFirstValue(KeyName.SESSION_ID);
            var aud = context.Principal?.FindFirstValue(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud);
            var tokenType = context.Principal?.FindFirstValue(TokenType.TOKEN_TYPE_TITLE);
            if (userId == null || sessionId == null || aud == null || tokenType == null)
            {
                context.Fail("Unauthorized: User authentication information error.");
            }
            else
            {
                bool isRefresh = false;
                var requestPath = context.HttpContext.Request.Path.Value; // 获取完整路径字符串
                if(requestPath != null)
                {
                    // 正则表达式，判断
                    var regex = new Regex(@"^/api/v\d+/auth/refresh$", RegexOptions.IgnoreCase);
                    var match = regex.Match(requestPath);
                    if (match.Success)
                    {
                        isRefresh = true;
                    }
                }
                // 从内存中获取用户Token信息
                if (!memoryCache.TryGetValue(int.Parse(userId), out UserTokenInfo? userTokenInfo))
                {
                    context.Fail("Unauthorized: The user session does not exist and needs to login again.");
                }
                else
                {
                    if ((aud == Audience.Browser && sessionId != userTokenInfo!.BrowserSession)
                    || (aud == Audience.Mobile && sessionId != userTokenInfo!.MobileSession))
                    {
                        context.Fail("Unauthorized: The Token has expired.");
                    }
                }

                if (tokenType == TokenType.RefreshToken) 
                { 
                    if(!isRefresh)
                    { 
                        context.Fail("Unauthorized: Wrong token type.");
                    }
                }
                else
                {
                    if (isRefresh)
                    {
                        context.Fail("Unauthorized: Wrong token type.");
                    } 
                } 
            } 
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.StatusCode = 440; 
                }
                else
                {
                    context.Response.StatusCode = 401;
                }
 
                var response = new ApiResponse<string>(context.Response.StatusCode, "Unauthorized", context.Exception.Message);
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
        ActionResult<object> response = (new ApiResponse<object>(HttpStatusCode.UnprocessableEntity, "Validation Failed!", errors)).Result();
        return response.Result!;
    }; 
}) ;
// 注册服务
builder.Services.AddScoped<ITopWindowService, TopWindowService>();
builder.Services.AddScoped<ITopWindowRepository, TopWindowRepository>(); 


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
// 使用健康检查中间件
// Configure the HTTP request pipeline.
app.UseHealthChecks("/health");
// 响应压缩
app.UseResponseCompression();
// 使用速率限制中间件
app.UseIpRateLimiting();
// 使用 CORS 策略
app.UseCors("AllowSpecificOrigin");


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
            405 => "Method Not Allowed",
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

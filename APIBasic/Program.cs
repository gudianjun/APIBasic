using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using APIBasic.Middleware;
 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using APIBasic.Common;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using APIBasic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ﾈﾕﾖｾｼﾇﾂｼ
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// ﾌ晴ﾓﾄﾚｴ貊ｺｴ豺ﾎ・
builder.Services.AddMemoryCache();
// ﾌ晴ﾓJWTﾉ昞ﾝﾑ鰒､ｷﾎ・
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
        OnForbidden = context =>
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 403;
                var response = new ApiResponse<string>(403, "Forbidden", "You are not authorized to access this resource.");
                return context.Response.WriteAsJsonAsync(response);
            }
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            // context.Token = context.Request.Headers["Authorization"];
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            context.Fail("Token is no longer valid.");
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
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 401;
                var response = new ApiResponse<string>(401, "Unauthorized", context.AuthenticateFailure?.Message?? "You are not authorized to access this resource.");
                return context.Response.WriteAsJsonAsync(response);
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>(); // ﾗ｢ｲ睚ｫｾﾖｹﾂﾋﾆ・
});

// Configure MySQL database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MySqlDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });

    // JWT 認証の設定を追加
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value.",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
   // c.OperationFilter<AddAuthorizationHeaderOperationFilter>();
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // 验证模型失败时返回自定义响应
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        var response = new ApiResponse<List<string>>((int)HttpStatusCode.BadRequest, "Validation errors occurred.", errors);
        return new BadRequestObjectResult(response);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication(); // ﾌ晴ﾓﾉ昞ﾝﾑ鰒､ﾖﾐｼ莨
app.UseAuthorization();

app.MapControllers();

// ﾗﾊﾔｴﾃｻﾓﾐﾕﾒｵｽﾊｱ
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

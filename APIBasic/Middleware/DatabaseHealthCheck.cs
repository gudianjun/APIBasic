using APIBasic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace APIBasic.Middleware
{
    /// <summary>
    /// 健康检查中间件
    /// </summary>
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public DatabaseHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            bool databaseIsHealthy = CheckDatabaseConnection();

            if (databaseIsHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Database is healthy"));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Database is unhealthy"));
        }

        private bool CheckDatabaseConnection()
        {
            // 使用数据库上下文检查数据库连接是否正常
            // 通过config中的连接字符串连接数据库，检查数据库连接是否正常
            // 连接数据库，检查数据库连接是否正常
            string? connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                return false;
            }
            else
            {
                try
                {
                    DbContextOptions<MySqlDbContext> options = new DbContextOptionsBuilder<MySqlDbContext>()
                       .UseMySql(connectionString!, ServerVersion.AutoDetect(connectionString))
                       .Options;
                    MySqlDbContext dbContext = new MySqlDbContext(options);
                    try
                    {
                        dbContext.Database.OpenConnection();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    finally
                    {
                        dbContext.Database.CloseConnection();
                    }
                }
                catch (Exception)
                {
                    return false;

                }
            }
            return true;
        }
    }
}

using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Enums;
using APIBasic.Models;
using APIBasic.Repositories.Interfaces; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace APIBasic.Repositories.Implementations
{
    public class TopWindowRepository : ITopWindowRepository
    {
        private readonly IConfiguration _configuration;
        private readonly MySqlDbContext _context;
        private readonly ILogger<TopWindowRepository> _logger;
        private readonly IMemoryCache _memoryCache;
        public TopWindowRepository(IConfiguration configuration, MySqlDbContext context
            , ILogger<TopWindowRepository> logger, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }
        public Task ChangePasswordAsync(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserInfoForUserNameAsync(string userName)
        {
            var rtn = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            // 通过UserName获取User表中的用户信息
            return rtn;
        }

        public async Task<User?> GetUserInfoForMailAddressAsync(string mailAddress)
        {
            var rtn = await _context.Users.FirstOrDefaultAsync(x => x.MailAddress == mailAddress);
            // 通过UserName获取User表中的用户信息
            return rtn;
        }

        public Task<User?> GetUserByIdAsync(uint userId)
        {
            var rtn = _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            return rtn;
        }

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task SaveUserAsync(User user)
        {
            throw new NotImplementedException();
        }
        public async  Task NewUserAsync(User user)
        {
            // 新增User表中的用户信息
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateUserAsync(User user)
        {
            // 更新User表中的用户信息
            _context.Users.Update(user);
            int count = await _context.SaveChangesAsync();
            return count;
        }

        public Task SaveLoginInfoAsync(int userId, string audience, string session)
        {
            if (!_memoryCache.TryGetValue(userId, out UserTokenInfo? userTokenInfo))
            {
                userTokenInfo = new UserTokenInfo();
            } // 根据audience更新相应的Token值
            if (audience == Audience.Browser)
            {
                userTokenInfo!.BrowserSession = session;
            }
            else if (audience == Audience.Mobile)
            {
                userTokenInfo!.MobileSession = session;
            }
            // 保存到内存中
            _memoryCache.Set(userId, userTokenInfo);
            return Task.CompletedTask;
        } 

        public void SaveResetPasswordCode(string email, string code)
        {
            // 保存验证码到内存中， 10分钟有效
            _memoryCache.Set(email, code.ToString(), TimeSpan.FromMinutes(10));
        }
        public string LoadResetPasswordCode(string email)
        {
            // 从内存中获取验证码
            if (_memoryCache.TryGetValue(email, out string? code))
            {
                return code!;
            }
            else
            {
                return string.Empty;
            }
        }


        public async Task<bool> CheckIfValueExistsAsync(string tableName, string columnName, object value)
        {
            // 获取 DbSet 属性
            var dbSetProperty = _context.GetType().GetProperty(tableName);
            if (dbSetProperty == null)
            {
                throw new ArgumentException($"Table '{tableName}' does not exist in the context.");
            }

            // 获取 DbSet 实例
            var dbSet = dbSetProperty.GetValue(_context) as IQueryable<object>;
            if (dbSet == null)
            {
                throw new ArgumentException($"Table '{tableName}' is not a valid DbSet.");
            }

            // 构建动态查询
            var parameter = Expression.Parameter(typeof(object), "x");
            var property = Expression.Property(Expression.Convert(parameter, dbSet.ElementType), columnName);
            var constant = Expression.Constant(value);
            var equal = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<object, bool>>(equal, parameter);

            // 执行查询
            return await dbSet.AnyAsync(lambda);
        }

        public async Task<List<DesignFile>> GetDesignFilesAsync(string deviceType, uint userId)
        {
            // 通过deviceType和userId获取DesignFile表中的设计文件信息，使用最后更新时间排序
            var rtn = await _context.DesignFiles.Where(x => x.DeviceType == deviceType
                && x.UserId == userId).OrderByDescending(x => x.LastUpdatedTime).ToListAsync();
            return rtn;
        }
        public async Task<DesignFile> GetDesignFileAsync(string deviceType
            , uint userId
            , string fileId)
        {
            // 通过deviceType和fileId获取DesignFile表中的设计文件信息
            var rtn = await _context.DesignFiles.Where(x => x.DeviceType == deviceType
                && x.UserId == userId && x.FileId == fileId).ToListAsync();

            if (rtn.Count > 0)
            {
                return rtn[0];
            }
            return null!;
        }
        public async Task<int> UpdateDesignFileAsync(DesignFile designFile)
        {
            // 更新DesignFile表中的设计文件信息
            _context.DesignFiles.Update(designFile);
            int count = await _context.SaveChangesAsync();
            return count;
        }
        public async Task<int> DeleteDesignFileAsync(string deviceType, uint userId, string fileId)
        {
            // 删除DesignFile表中的设计文件信息
            var designFile = await _context.DesignFiles.FirstOrDefaultAsync(x => x.DeviceType == deviceType
                && x.UserId == userId && x.FileId == fileId);
            if (designFile != null)
            {
                _context.DesignFiles.Remove(designFile);
                int count = await _context.SaveChangesAsync();
                return count;
            }
            return 0;
        }
        public async Task<int> AddDesignFileAsync(DesignFile designFile)
        {
            // 新增DesignFile表中的设计文件信息
            await _context.DesignFiles.AddAsync(designFile);
            int count = await _context.SaveChangesAsync();
            return count;
        }
    }
}

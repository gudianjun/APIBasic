using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Enums;
using APIBasic.Models;
using APIBasic.Repositories.Interfaces; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

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

        public Task<GetDesignDetailsResponse> GetDesignDetailsAsync(int designId)
        {
            throw new NotImplementedException();
        }

        public Task<GetDesignsResponse> GetDesignsAsync(GetDesignsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetTypeDesignsResponse>> GetTypeDesignsAsync(GetTypeDesignsRequest request)
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
    }
}

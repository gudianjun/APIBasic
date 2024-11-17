using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Models;
using APIBasic.Repositories.Interfaces;
using APIBasic.Services.Implementations;
using Microsoft.Extensions.Caching.Memory;

namespace APIBasic.Repositories.Implementations
{
    public class TopWindowRepository : ITopWindowRepository
    { 
        private readonly IConfiguration _configuration;
        private readonly MySqlDbContext _context;
        private readonly ILogger<TopWindowRepository> _logger;
        private readonly IMemoryCache _memoryCache;
        public TopWindowRepository(IConfiguration configuration, MySqlDbContext context,
            ITopWindowRepository topWindowRepository
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

        public Task<User?> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task SaveLoginInfoAsync(Logininfo logininfo)
        {
            throw new NotImplementedException();
        }

        public Task SaveUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}

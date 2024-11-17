using APIBasic.Controllers;
using APIBasic.Data;
using APIBasic.DTOs;
using APIBasic.Repositories.Interfaces;
using APIBasic.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace APIBasic.Services.Implementations
{
    public class TopWindowService : ITopWindowService
    {
        private readonly ITopWindowRepository  _topWindowRepository;
        private readonly IConfiguration _configuration; 
        private readonly ILogger<TopWindowService> _logger;
        private readonly IMemoryCache _memoryCache;
        public TopWindowService(IConfiguration configuration,  
            ITopWindowRepository topWindowRepository
            , ILogger<TopWindowService> logger, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _topWindowRepository = topWindowRepository;
            _configuration = configuration; 
        }

        public Task<ChangePasswordResponse> ChangePasswordAsync(string userId, ChangePasswordRequest request)
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

        public Task<GetTypeDesignsResponse> GetTypeDesignsAsync(GetTypeDesignsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserInfoResponse> GetUserInfoAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserInfoResponse> UpdateUserInfoAsync(UpdateUserInfoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

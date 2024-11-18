using APIBasic.DTOs;
using APIBasic.Models;

namespace APIBasic.Repositories.Interfaces
{
    public interface ITopWindowRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
 
        Task<IEnumerable<GetTypeDesignsResponse>> GetTypeDesignsAsync(GetTypeDesignsRequest request);
        Task<GetDesignsResponse> GetDesignsAsync(GetDesignsRequest request);
        Task<GetDesignDetailsResponse> GetDesignDetailsAsync(int designId);
        Task<User?> GetUserInfoForUserNameAsync(string userName);
        Task<User?> GetUserByIdAsync(string userId);
        Task SaveUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task ChangePasswordAsync(string userId, string newPassword);

        Task SaveLoginInfoAsync(int userId, string audience, string token);
    }
}

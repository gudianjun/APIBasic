using APIBasic.DTOs;
using APIBasic.Models;

namespace APIBasic.Repositories.Interfaces
{
    public interface ITopWindowRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
  
        Task<User?> GetUserInfoForUserNameAsync(string userName);
        Task<User?> GetUserInfoForMailAddressAsync(string mailAddress);
        Task<User?> GetUserByIdAsync(uint userId);
        Task SaveUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        Task ChangePasswordAsync(string userId, string newPassword);

        Task SaveLoginInfoAsync(int userId, string audience, string token);

        void SaveResetPasswordCode(string email, string code);
        string LoadResetPasswordCode(string email);
    }
}

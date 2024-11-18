using APIBasic.DTOs;
using APIBasic.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIBasic.Services.Interfaces
{
    public interface ITopWindowService
    {
        Task<IActionResult>  LoginAsync(LoginRequest request);
        Task LogoutAsync();
        Task<GetTypeDesignsResponse> GetTypeDesignsAsync(GetTypeDesignsRequest request);
        Task<GetDesignsResponse> GetDesignsAsync(GetDesignsRequest request);
        Task<GetDesignDetailsResponse> GetDesignDetailsAsync(int designId);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<GetUserInfoResponse> GetUserInfoAsync(string userId);
        Task<UpdateUserInfoResponse> UpdateUserInfoAsync(UpdateUserInfoRequest request);
        Task<ChangePasswordResponse> ChangePasswordAsync(string userId, ChangePasswordRequest request);
         
    }
}

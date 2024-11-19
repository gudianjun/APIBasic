using APIBasic.DTOs;
using APIBasic.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIBasic.Services.Interfaces
{
    public interface ITopWindowService
    {
        Task<ActionResult<LoginResponse>>  LoginAsync(LoginRequest request);
        Task LogoutAsync(); 
        Task<ActionResult<LoginResponse>> RefreshAsync();

        Task<GetTypeDesignsResponse> GetTypeDesignsAsync(GetTypeDesignsRequest request);
        Task<GetDesignsResponse> GetDesignsAsync(GetDesignsRequest request);
        Task<GetDesignDetailsResponse> GetDesignDetailsAsync(int designId);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<GetUserInfoResponse> GetUserInfoAsync(string userId);
        Task<ActionResult<UpdateUserInfoResponse>> UpdateUserInfoAsync(UpdateUserInfoRequest request);
        Task<ActionResult<ChangePasswordResponse>> ChangePasswordAsync(ChangePasswordRequest request); 
    }
}

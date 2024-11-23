using APIBasic.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace APIBasic.Services.Interfaces
{
    public interface ITopWindowService
    {
        Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request);
        Task LogoutAsync();
        Task<ApiResponse<LoginResponse>> RefreshAsync();
        Task<ApiResponse<RegisterResponse>> RegisterAsync(RegisterRequest request);
        Task<GetUserInfoResponse> GetUserInfoAsync(string userId);
        Task<ApiResponse<UpdateUserInfoResponse>> UpdateUserInfoAsync(UpdateUserInfoRequest request);
        Task<ApiResponse<ChangePasswordResponse>> ChangePasswordAsync(ChangePasswordRequest request);


        Task<ApiResponse<GetFilesResponse>> GetFilesAsync();
        Task<ApiResponse<DownloadFileResponse>> DownloadFileAsync(string fileId);
        Task<ApiResponse<CreateFileResponse>> CreateFileAsync(CreateFileRequest request);
        Task<ApiResponse<DeleteFileResponse>> DeleteFileAsync(string fileId);
        Task<ApiResponse<UpdateFileResponse>> UpdateFileAsync(string fileId, UpdateFileRequest request);

        Task<ApiResponse<SendResetPasswordCodeResponse>> SendResetPasswordCodeAsync(SendResetPasswordCodeRequest request);
        Task<ApiResponse<CodeResetPasswordResponse>> CodeResetPasswordAsync(CodeResetPasswordRequest request);

        Task<ApiResponse<SendCodeResponse>> SendCodeAsync([FromBody] SendCodeRequest request);

        Task<bool> CheckMailExist(string? mail);
    }
}

using APIBasic.Models;

namespace APIBasic.Repositories.Interfaces
{
    public interface ITopWindowRepository
    {
        Task<User?> GetUserInfoForUserNameAsync(string userName);
        Task<User?> GetUserInfoForMailAddressAsync(string mailAddress);
        Task<User?> GetUserByIdAsync(uint userId);
        Task NewUserAsync(User user);
        Task<int> UpdateUserAsync(User user);
        Task SaveLoginInfoAsync(int userId, string audience, string token);
        void SaveResetPasswordCode(string email, string code);
        string LoadResetPasswordCode(string email);
        Task<bool> CheckIfValueExistsAsync(string tableName, string columnName, object value);
        #region DesignFile
        Task<List<DesignFile>> GetDesignFilesAsync(string deviceType, uint userId);
        Task<DesignFile> GetDesignFileAsync(string deviceType, uint userId, string fileId);
        Task<int> UpdateDesignFileAsync(DesignFile designFile);
        Task<int> DeleteDesignFileAsync(string deviceType, uint userId, string fileId);
        Task<int> AddDesignFileAsync(DesignFile designFile);
        Task<DesignFile?> GetDesignFileForIDAsync(string fileId);
        #endregion

    }
}

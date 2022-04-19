using TheNewFora.Shared;

namespace TheNewFora.Client.Services
{
    public interface IApplicationUsersManager
    {
        Task<string> AddUserAsync(ApplicationUser user);
        Task<string> ChangePasswordAsync(UserDto PasswordDto);
        Task<string> DeleteUserAsync(ApplicationUser user);
        Task<List<ApplicationUser>?> GetAllUsersAsync();
        Task<bool> GetBanDeleteFlagAsync(string id);
        Task<ApplicationUser?> GetUserAsync(string id);
        Task<string?> GetUserImageAsync(string id);
        Task<string?> GetUserNameByIdAsync(string id);
        Task<string> UpdateUserAsync(ApplicationUser user);
    }
}
using TheNewFora.Shared;

namespace TheNewFora.Client.Services
{
    public interface IUserInterestsManager
    {
        Task<string> AddUserInterestAsync(UserInterestModel userInterest);
        Task<string> DeleteUserInterestAsync(UserInterestModel userInterest);
        Task<List<UserInterestModel>?> GetAllUserInterestsAsync();
        Task<List<UserInterestModel>?> GetAllUserInterestsByIdAsync(string id);
        Task<UserInterestModel?> GetUserInterestAsync(int id);
        Task<string> UpdateUserInterestAsync(UserInterestModel userInterest);
    }
}
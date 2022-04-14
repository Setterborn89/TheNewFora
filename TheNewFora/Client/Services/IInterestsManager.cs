using TheNewFora.Shared;

namespace TheNewFora.Client.Services
{
    public interface IInterestsManager
    {
        Task<string> AddInterestAsync(InterestModel interest);
        Task<string> DeleteInterestAsync(InterestModel interest);
        Task<List<InterestModel>?> GetAllInterestsAsync();
        Task<InterestModel?> GetInterestAsync(int id);
        Task<string?> GetInterestNameAsync(int id);
        Task<int> GetNumberOfThreadsByInterestId(int id);
        Task<string> UpdateInterestAsync(InterestModel interest);
    }
}
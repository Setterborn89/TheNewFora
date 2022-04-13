using TheNewFora.Shared;

namespace TheNewFora.Client.Services
{
    public interface IThreadsManager
    {
        Task<string> AddThreadAsync(ThreadModel thread);
        Task<string> DeleteThreadAsync(ThreadModel thread);
        Task<List<ThreadModel>?> GetAllThreadsAsync();
        Task<int> GetNumberOfPostsByThreadId(int id);
        Task<ThreadModel?> GetThreadAsync(int id);
        Task<List<ThreadModel>?> GetThreadsByInterestIdAsync(int id);
        Task<string> UpdateThreadAsync(ThreadModel thread);
    }
}
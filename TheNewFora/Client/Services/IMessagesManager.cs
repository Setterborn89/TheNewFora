using TheNewFora.Shared;

namespace TheNewFora.Client.Services
{
    public interface IMessagesManager
    {
        Task<string> AddMessageAsync(MessageModel message);
        Task<string> DeleteMessageAsync(int id);
        Task<List<MessageModel>?> GetAllMessagesAsync();
        Task<MessageModel?> GetMessageAsync(int id);
        Task<string> UpdateMessageAsync(MessageModel message);
    }
}
using TheNewFora.Shared;

namespace TheNewFora.Client.Services
{
    public class MessagesManager : IMessagesManager
    {
        private readonly HttpClient _client;

        public MessagesManager(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<MessageModel>?> GetAllMessagesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync($"api/messages");
            var message = await response.Content.ReadAsAsync<List<MessageModel>>();
            return response.IsSuccessStatusCode ? message : null;
        }
        public async Task<MessageModel?> GetMessageAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/messages/{id}");
            var message = await response.Content.ReadAsAsync<MessageModel>();
            return response.IsSuccessStatusCode ? message : null;
        }
        public async Task<string> AddMessageAsync(MessageModel message)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/messages", message);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
        public async Task<string> UpdateMessageAsync(MessageModel message)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/messages", message);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
        public async Task<string> DeleteMessageAsync(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/messages/{id}");
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
    }
}

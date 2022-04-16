using TheNewFora.Shared;
using System.Net.Http.Json;

namespace TheNewFora.Client.Services
{
    public class ThreadsManager : IThreadsManager
    {
        private readonly HttpClient _client;

        public ThreadsManager(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ThreadModel>?> GetAllThreadsAsync()
        {
            return await _client.GetFromJsonAsync<List<ThreadModel>>($"api/threads");
        }
        public async Task<ThreadModel?> GetThreadAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/threads/{id}");
            var thread = await response.Content.ReadAsAsync<ThreadModel>();
            return response.IsSuccessStatusCode ? thread : null;
        }
        public async Task<List<ThreadModel>?> GetThreadsByInterestIdAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/threads/getbyinterestid?id={id}");
            var threadList = await response.Content.ReadAsAsync<List<ThreadModel>>();
            return response.IsSuccessStatusCode ? threadList : null;
        }
        public async Task<int> GetNumberOfPostsByThreadId(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/threads/getpostsbyid?id={id}");
            int count = await response.Content.ReadAsAsync<int>();
            return response.IsSuccessStatusCode ? count : 0;
        }
        public async Task<string> AddThreadAsync(ThreadModel thread)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/threads", thread);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
        public async Task<string> UpdateThreadAsync(ThreadModel thread)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/threads", thread);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
        public async Task<string> DeleteThreadAsync(ThreadModel thread)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/threads/{thread.Id}");
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
    }
}

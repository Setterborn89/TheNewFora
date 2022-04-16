using System.Text;
using System.Text.Json;
using TheNewFora.Shared;

namespace TheNewFora.Client.Services
{
    public class UserInterestsManager : IUserInterestsManager
    {
        private readonly HttpClient _client;

        public UserInterestsManager(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<UserInterestModel>?> GetAllUserInterestsByIdAsync(string id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/userinterests?id={id}");
            var userInterest = await response.Content.ReadAsAsync<List<UserInterestModel>>();
            return response.IsSuccessStatusCode ? userInterest : null;
        }
        public async Task<List<UserInterestModel>?> GetAllUserInterestsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync($"api/userinterests/getall");
            var userInterest = await response.Content.ReadAsAsync<List<UserInterestModel>>();
            return response.IsSuccessStatusCode ? userInterest : null;
        }

        public async Task<UserInterestModel?> GetUserInterestAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/userinterests/{id}");
            var userInterest = await response.Content.ReadAsAsync<UserInterestModel>();
            return response.IsSuccessStatusCode ? userInterest : null;
        }
        public async Task<string> AddUserInterestAsync(UserInterestModel userInterest)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/userinterests", userInterest);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
        public async Task<string> UpdateUserInterestAsync(UserInterestModel userInterest)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/userinterests", userInterest);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
        public async Task<string> DeleteUserInterestAsync(UserInterestModel userInterest)
        {
            var httpMessage = new HttpRequestMessage(HttpMethod.Delete, "api/userinterests/")
            {
                Content = new StringContent(JsonSerializer.Serialize(userInterest), Encoding.UTF8, "application/json")
            };
            var response = await _client.SendAsync(httpMessage);

            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
    }
}

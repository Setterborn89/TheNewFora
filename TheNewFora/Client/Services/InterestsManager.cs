using TheNewFora.Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace TheNewFora.Client.Services
{
    public class InterestsManager : IInterestsManager
    {
        private readonly HttpClient _client;

        public InterestsManager(HttpClient client)
        {
            _client = client;
        }

        //  Database Calls

        public async Task<List<InterestModel>> GetAllInterestsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync($"api/interests");
            var list = await response.Content.ReadAsAsync<List<InterestModel>>();
            return response.IsSuccessStatusCode ? list : null;
        }
        public async Task<InterestModel?> GetInterestAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/interests/{id}");
            var interest = await response.Content.ReadAsAsync<InterestModel>();
            return response.IsSuccessStatusCode ? interest : null;
        }
        public async Task<string?> GetInterestNameAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/interests/getinterestname?id={id}");
            var interestName = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode ? interestName : null;
        }
        public async Task<int> GetNumberOfThreadsByInterestId(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/interests/getthreadsbyid?id={id}");
            int count = await response.Content.ReadAsAsync<int>();
            return response.IsSuccessStatusCode ? count : 0;
        }
        public async Task<string> AddInterestAsync(InterestModel interest)
        {
            var Content = new StringContent(JsonSerializer.Serialize(interest), System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync($"api/interests", Content);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";

        }
        public async Task<string> UpdateInterestAsync(InterestModel interest)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/interests", interest);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
        public async Task<string> DeleteInterestAsync(InterestModel interest)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/interests/{interest.Id}");
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
    }
}


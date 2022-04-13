using TheNewFora.Shared;

namespace TheNewFora.Client.Services
{
    public class ApplicationUsersManager : IApplicationUsersManager
    {
        private readonly HttpClient _client;

        public ApplicationUsersManager(HttpClient client)
        {
            _client = client;
        }

        //  Database Calls

        public async Task<List<ApplicationUser>?> GetAllUsersAsync()
        {
            HttpResponseMessage response = await _client.GetAsync($"api/users");
            var user = await response.Content.ReadAsAsync<List<ApplicationUser>>();
            return response.IsSuccessStatusCode ? user : null;
        }
        public async Task<ApplicationUser?> GetUserAsync(string id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/users/{id}");
            var user = await response.Content.ReadAsAsync<ApplicationUser>();
            return response.IsSuccessStatusCode ? user : null;
        }
        public async Task<string?> GetUserNameByIdAsync(string id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/users/getuserbyid?id={id}");
            var userName = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode ? userName : null;
        }
        public async Task<string?> GetUserImageAsync(string id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/users/getuserimagebyid?id={id}");
            var imageUrl = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode ? imageUrl : null;
        }
        public async Task<bool> GetBanDeleteFlagAsync(string id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/users/getbanflag?id={id}");
            var banflag = await response.Content.ReadAsAsync<bool>();
            return response.IsSuccessStatusCode ? banflag : false;
        }
        public async Task<ApplicationUser?> GetDetachedUserAsync(string id)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/users/{id}");
            var user = await response.Content.ReadAsAsync<ApplicationUser>();
            return response.IsSuccessStatusCode ? user : null;
        }
        public async Task<string> AddUserAsync(ApplicationUser user)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/users", user);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
        public async Task<string> UpdateUserAsync(ApplicationUser user)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/users", user);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
        public async Task<string> DeleteUserAsync(ApplicationUser user)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/users/{user.Id}");
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }
        public async Task<string> ChangePasswordAsync(UserDto PasswordDto)
        {
            var response = await _client.PostAsJsonAsync<UserDto>("api/auth/changepassword", PasswordDto);
            return response.IsSuccessStatusCode ? "Successful" : "Failed";
        }


    }
}

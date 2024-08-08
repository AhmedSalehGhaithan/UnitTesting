using API.Models;
using API.Repository;

namespace API.Services
{
    public class UserService : IUserServiceRepository
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri("https://localhost:7289");
        }
        public async Task<bool> CreateAsync(User user)
        {
            var result = await _httpClient.PostAsJsonAsync("/user/add", user);
            if (result.IsSuccessStatusCode) return true;
            else
                return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _httpClient.GetAsync($"/user/delete/{id}");
            if (result.IsSuccessStatusCode) return true;
            else
                return false;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _httpClient.GetFromJsonAsync<IEnumerable<User>>("User/get");

        public async Task<User> GetById(int id) => await _httpClient.GetFromJsonAsync<User>($"User/Get-single/{id}");

        public async Task<bool> UpdateAsync(User user)
        {
            var result = await _httpClient.PutAsJsonAsync("User/update", user);
            if (result.IsSuccessStatusCode) return true;
            else
                return false;
        }
    }
}

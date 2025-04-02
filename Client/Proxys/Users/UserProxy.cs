using System.Text.Json;
using Functions.Shared.DTOs.Users;
using Functions.Shared.Interfaces.User;

namespace Functions.Client.Proxys.Users
{
    public class UserProxy(HttpClient httpClient) : IUserProxy
    {
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var response = await httpClient.GetAsync("api/user/getallusers");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<UserDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            var response = await httpClient.GetAsync($"api/user/getuserbyid?id={id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}

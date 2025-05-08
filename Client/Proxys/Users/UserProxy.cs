using Functions.Client.Services;
using Functions.Shared.DTOs.Auth;
using Functions.Shared.DTOs.Users;
using Functions.Shared.Interfaces.User;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Functions.Client.Proxys.Users
{
    public class UserProxy(HttpClient httpClient,
                           AuthService authService) : IUserProxy
    {

        public async Task<UserDTO> GetUser()
        {
            var userToken = await authService.GetToken() ?? throw new UnauthorizedAccessException();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var response = await httpClient.GetAsync($"api/user/getuser");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<HttpResponseMessage> UpdateUser(UpdateUserRequestDTO updateUserRequestDTO)
        {
            var userToken = await authService.GetToken() ?? throw new UnauthorizedAccessException();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            return await httpClient.PutAsJsonAsync("api/user/updateuser", updateUserRequestDTO);
        }
    }
}

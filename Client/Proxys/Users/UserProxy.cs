using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Functions.Shared.DTOs.Users;
using Functions.Shared.Interfaces.User;

namespace Functions.Client.Proxys.Users
{
    public class UserProxy(HttpClient httpClient) : IUserProxy
    {

        public async Task<UserDTO> GetUserById(Guid id)
        {
            var response = await httpClient.GetAsync($"api/user/getuserbyid?id={id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<ProfilePictureDTO> GetUserProfilePicture(Guid id)
        {
            var response = await httpClient.GetAsync($"api/user/getuserprofilepicture?id={id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStreamAsync();
            return JsonSerializer.Deserialize<ProfilePictureDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<HttpResponseMessage> UpdaetUser(UpdateUserRequestDTO updateUserRequestDTO)
        {
            return await httpClient.PostAsJsonAsync("api/user/updateuser", updateUserRequestDTO);
        }
    }
}

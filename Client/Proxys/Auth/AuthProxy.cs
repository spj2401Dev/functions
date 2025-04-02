using Functions.Shared.DTOs.Auth;
using Functions.Shared.Interfaces.Auth;
using System.Net.Http.Json;

namespace Functions.Client.Proxys.Auth
{
    public class AuthProxy : IAuthProxy
    {
        private readonly HttpClient _httpClient;

        public AuthProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> Register(RegisterRequestDTO request)
        {
            return await _httpClient.PostAsJsonAsync("api/auth/register", request);
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            }

            throw new HttpRequestException("Authentication failed");
        }
    }
}

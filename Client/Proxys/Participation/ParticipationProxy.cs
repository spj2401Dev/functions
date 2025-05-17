using System.Net.Http.Json;
using System.Text.Json;
using Functions.Client.Services;
using Functions.Shared.DTOs.Participation;
using Functions.Shared.Interfaces.Participation;
using System.Net.Http.Headers;

namespace Functions.Client.Proxys.Participation
{
    public class ParticipationProxy(HttpClient httpClient, AuthService authService) : IParticipationProxy
    {
        public async Task PostParticipation(PostParticipationDTO request)
        {
            var userToken = await authService.GetToken();
            if (string.IsNullOrEmpty(userToken))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var response = await httpClient.PostAsJsonAsync("api/participation/postparticipation", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<GetParticipationResponseDTO> GetParticipaton(Guid EventId)
        {
            // User might be unauthenticated, but here we allow this. If user isnt authed, we do not reurn per user result.
            string? userToken = await authService.GetToken();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var response = await httpClient.GetAsync($"api/participation/getparticipation?EventId={EventId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetParticipationResponseDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new GetParticipationResponseDTO();
        }
    }
}

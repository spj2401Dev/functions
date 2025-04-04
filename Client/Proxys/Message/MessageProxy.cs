using Functions.Client.Services;
using Functions.Shared.DTOs.Messages;
using Functions.Shared.Interfaces.Messages;
using System.Text.Json;
using System.Text;
namespace Functions.Client.Proxys.Message
{
    public class MessageProxy(HttpClient httpClient,
                              AuthService authService) : IMessageProxy
    {
        public async Task<HttpResponseMessage> PostAnnouncement(AnnouncementRequestDTO request)
        {
            var userToken = await authService.GetToken();
            if (string.IsNullOrEmpty(userToken))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var json = JsonSerializer.Serialize(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);

            var response = await httpClient.PostAsync("api/messages/PostAnnouncement", data);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<List<MessageDTO>> GetMessagesForEvent(Guid eventId)
        {
            var response = await httpClient.GetAsync($"api/messages/GetMessagesForEvent?eventId={eventId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<MessageDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<MessageDTO>();
        }
    }
}

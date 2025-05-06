using Functions.Client.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces;
using System.Text;
using System.Text.Json;

namespace Functions.Client.Proxys.Events
{
    public class EventProxy(HttpClient httpClient,
                            AuthService authService) : IEventsProxy
    {
        public async Task<List<EventMasterPageDTO>> GetEventsAsync()
        {
            var response = await httpClient.GetAsync("api/events/getEvents");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<EventMasterPageDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<HttpResponseMessage> PostEventAsync(EventsDTO request)
        {
            var userToken = await authService.GetToken();
            if (string.IsNullOrEmpty(userToken))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var json = JsonSerializer.Serialize(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);

            var response = await httpClient.PostAsync("api/events", data);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<EventsDTO?> GetEventById(Guid Id)
        {
            var response = await httpClient.GetAsync($"api/events/getEventbyId?id={Id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EventsDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<EventMasterPageDTO>> GetAllEventsByUserAsync()
        {
            var userToken = await authService.GetToken();
            if (string.IsNullOrEmpty(userToken))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);

            var response = await httpClient.GetAsync("api/events/getalleventsbyuser");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<EventMasterPageDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<HttpResponseMessage> PutEventAsync(EventsDTO request)
        {
            var userToken = await authService.GetToken();
            if (string.IsNullOrEmpty(userToken))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userToken);

            var json = JsonSerializer.Serialize(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync("api/events/putEventById", data);
            
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}

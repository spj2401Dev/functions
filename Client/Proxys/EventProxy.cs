﻿using Functions.Client.Services;
using Functions.Shared.DTOs;
using Functions.Shared.Interfaces;
using System.Text;
using System.Text.Json;

namespace Functions.Client.Proxys
{
    public class EventProxy(HttpClient httpClient,
                            AuthService authService) : IEventsProxy
    {
        public async Task<List<EventsDTO>> GetEventsAsync()
        {
            var response = await httpClient.GetAsync("api/events/getEvents");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<EventsDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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

        public async Task<EventsDTO?> GetEventsbyIdAsync(Guid Id)
        {
            var response = await httpClient.GetAsync($"api/events/getEventbyId?id={Id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EventsDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<EventsDTO>> GetPublicEventsAsync()
        {
            var response = await httpClient.GetAsync($"api/Events/getpublicevents");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<EventsDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}

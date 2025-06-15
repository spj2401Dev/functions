using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces.Search;
using System.Text.Json;

namespace Functions.Client.Proxys.Search
{
    public class SearchProxy(HttpClient httpClient) : ISearchProxy
    {
        public async Task<IEnumerable<EventMasterPageDTO>> Search(string query)
        {
            var response = await httpClient.GetAsync($"api/Search/Search?query={query}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<EventMasterPageDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<EventMasterPageDTO>();
        }
    }
}

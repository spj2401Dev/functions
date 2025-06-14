using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces.Search;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages.Search
{
    public partial class Search
    {
        [Inject] private ISearchProxy searchProxy { get; set; } = default!;
        [Inject] private NavigationManager navigationManager { get; set; } = default!;

        private string searchQuery = string.Empty;
        private List<EventMasterPageDTO> searchResults = new();
        private bool isSearching = false;

        private async Task OnSearchInput(ChangeEventArgs e)
        {
            searchQuery = e.Value?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(searchQuery) || searchQuery.Length <= 3)
            {
                searchResults.Clear();
                StateHasChanged();
                return;
            }

            isSearching = true;
            StateHasChanged();

            try
            {
                Console.WriteLine("Searching for: " + searchQuery);
                var results = await searchProxy.Search(searchQuery);
                searchResults = results.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Search error: {ex.Message}");
                searchResults.Clear();
            }
            finally
            {
                isSearching = false;
                StateHasChanged();
            }
        }

        private void NavigateToEventDetail(Guid eventId)
        {
            navigationManager.NavigateTo($"/events/{eventId}");
        }
    }
}
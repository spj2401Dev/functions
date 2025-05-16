using Functions.Client.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages
{
    public partial class Index
    {
        [Inject] IEventsProxy eventsProxy { get; set; } = default!;
        [Inject] AuthService authService { get; set; } = default!;
        [Inject] NavigationManager navigationManager { get; set; } = default!;

        private HomePageResponseDTO homePageData { get; set; } = new();
        private bool isAuthenticated = false;

        protected override async Task OnInitializedAsync()
        {
            isAuthenticated = await authService.IsAuthenticated();
            if (isAuthenticated)
            {
                await LoadHomePageData();
            } 
            else
            {
                navigationManager.NavigateTo("/events");
            }
        }

        private async Task LoadHomePageData()
        {
            homePageData = await eventsProxy.GetAllEventsByUserAsync();
        }
    }
}

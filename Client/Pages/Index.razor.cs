using Functions.Client.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.DTOs.Users;
using Functions.Shared.Interfaces;
using Functions.Shared.Interfaces.User;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages
{
    public partial class Index
    {
        [Inject] IEventsProxy eventsProxy { get; set; } = default!;
        [Inject] AuthService authService { get; set; } = default!;
        [Inject] NavigationManager navigationManager { get; set; } = default!;
        [Inject] IUserProxy userProxy { get; set; } = default!;

        private HomePageResponseDTO homePageData { get; set; } = new();
        private UserDTO authedUser { get; set; } = new();
        private bool isAuthenticated = false;

        protected override async Task OnInitializedAsync()
        {
            isAuthenticated = await authService.IsAuthenticated();
            if (isAuthenticated)
            {
                await LoadHomePageData();
                await LoadCurrentUser();
            } 
            else
            {
                navigationManager.NavigateTo("/landing");
            }
        }

        private async Task LoadHomePageData()
        {
            homePageData = await eventsProxy.GetAllEventsByUserAsync();
        }

        private async Task LoadCurrentUser()
        {
            authedUser = await userProxy.GetUser();
        }
    }
}

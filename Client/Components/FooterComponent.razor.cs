using Functions.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Components
{
    public partial class FooterComponent
    {
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private AuthService authService { get; set; }

        private bool isAuthenticated;
        private string username;

        protected override async Task OnInitializedAsync()
        {
            isAuthenticated = await authService.IsAuthenticated();
            if (isAuthenticated)
            {
                username = "@" + await authService.GetUsername();
            }
        }

        private void RedirectToNewEvent()
        {
            if (isAuthenticated)
            {
                navigationManager.NavigateTo("events/new", false);
            }
            else
            {
                navigationManager.NavigateTo("login/events/new", false);
            }
        }
    }
}
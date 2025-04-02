using Functions.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Components
{
    public partial class HeaderComponent
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

        private void NavigateToRegister()
        {
            navigationManager.NavigateTo("register", false);
        }

        private void NavigateToLogin()
        {
            navigationManager.NavigateTo("login", false);
        }

        private async Task Logout()
        {
            await authService.Logout();
            navigationManager.Refresh(true);
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
    //} left out bracket on purpose for Github Gated Committee test
}

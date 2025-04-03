using Functions.Client.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

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
            await base.OnInitializedAsync(); 
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
            navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
        }

        private void RedirectToNewEvent()
        {
            if (isAuthenticated)
            {
                navigationManager.NavigateTo("events/new", false);
            }
            else
            {
                var returnUrl = System.Net.WebUtility.UrlEncode("events/new");
                navigationManager.NavigateTo($"login?returnUrl={returnUrl}", false);
            }
        }
    }
}
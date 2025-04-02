namespace Functions.Client.Pages.Auth
{
    using Functions.Client.Services;
    using Functions.Shared.DTOs.Auth;
    using Functions.Shared.Interfaces.Auth;
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    public partial class Login
    {
        [Parameter] public string? ReturnUrl { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private IAuthProxy authProxy { get; set; } = default!;
        [Inject] private AuthService authService { get; set; } = default!;

        private LoginRequestDTO loginRequest = new();
        private string? message;
        private bool isLoading = false;

        private bool isAuthenticated; // Debugging :)
        private string? username;
        private string? userId;

        protected override async Task OnInitializedAsync() 
        {
            isAuthenticated = await jwtAuthService.IsAuthenticated();
            username = await jwtAuthService.GetUsername();
            userId = await jwtAuthService.GetUserId();
        } // End of Debugging :P

        private async Task HandleLogin()
        {
            isLoading = true;
            message = null;

            try
            {
                var response = await authProxy.Login(loginRequest);

                await authService.StoreAuthInfo(response);

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/", true);
                }
                else
                {
                    navigationManager.NavigateTo(ReturnUrl, true);
                }
            }
            catch
            {
                message = "Login failed. Check your username and password.";
            }
            finally
            {
                isLoading = false;
            }
        }

        private async Task Logout()
        {
            await authService.Logout();
            navigationManager.Refresh(true);
        }

        private void NavigateToRegisterPage()
        {
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                navigationManager.NavigateTo("/register", false);
            }
            else
            {
                navigationManager.NavigateTo("/register/" + ReturnUrl, false);
            }
        }
    }
}

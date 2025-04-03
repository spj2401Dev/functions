using Functions.Client.Services;
using Functions.Shared.DTOs.Auth;
using Functions.Shared.Interfaces.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace Functions.Client.Pages.Auth
{
    public partial class Register
    {
        [Parameter] public string? ReturnUrl { get; set; }
        [Inject] private IAuthProxy authProxy { get; set; } = default!;
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private AuthService authService { get; set; } = default!;
        private RegisterRequestDTO request = new();
        private string? message;
        private bool isLoading = false;
        private string? confirmPassword;


        private async Task CreateUser()
        {
            isLoading = true;

            if (request.Password != confirmPassword)
            {
                message = "Passwords do not match";
                return;
            }

            var response = await authProxy.Register(request);

            if (response.IsSuccessStatusCode)
            {
                message = "Account wurde erfolgreich erstellt.";

                var loginRequest = new LoginRequestDTO
                {
                    Username = request.Username,
                    Password = request.Password
                };

                var loginResponse = await authProxy.Login(loginRequest);

                await authService.StoreAuthInfo(loginResponse);

                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/", true);
                }
                else
                {
                    navigationManager.NavigateTo(ReturnUrl, true);
                }
            }
            else
            {
                message = "Es ist ein fehler aufgetreten!";
            }
        }
    }
}

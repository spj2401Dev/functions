using Functions.Shared.DTOs.Auth;
using Functions.Shared.Interfaces.Auth;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages.Auth
{
    public partial class Register
    {
        [Inject] private IAuthProxy authProxy { get; set; } = default!;
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
                request = new RegisterRequestDTO();
            }
            else
            {
                message = "Es ist ein fehler aufgetreten!";
            }

        }
    }
}

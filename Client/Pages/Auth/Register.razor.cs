using Functions.Client.Services;
using Functions.Shared.DTOs.Auth;
using Functions.Shared.DTOs.Users;
using Functions.Shared.Interfaces.Auth;
using Functions.Shared.Interfaces.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Functions.Client.Pages.Auth
{
    public partial class Register
    {
        [Parameter] public string? ReturnUrl { get; set; }
        [Inject] private IAuthProxy authProxy { get; set; } = default!;
        [Inject] private IUserProxy userProxy { get; set; } = default!;
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private AuthService authService { get; set; } = default!;
        private RegisterRequestDTO request = new();
        private string? message;
        private bool isLoading = false;
        private string? confirmPassword;
        private bool isEditingUser = false;
        private UserDTO user = new();


        protected override async Task OnInitializedAsync()
        {
            var path = new Uri(navigationManager.Uri).AbsolutePath;

            if (path == "/edituser")
            {
                if (!await authService.IsAuthenticated())
                {
                    navigationManager.NavigateTo("/login/edituser");
                }

                isEditingUser = true;

                user = await userProxy.GetUser();
                request.UserName = user.Username;
                request.FirstName = user.Firstname;
                request.LastName = user.Lastname;
                request.Email = user.Email;
            }
        }

        private async Task CreateUser()
        {
            isLoading = true;

            if (request.Password != confirmPassword && (request.Password != null || request.Password != string.Empty))
            {
                message = "Passwords do not match or is not set.";
                return;
            }

            if (isEditingUser)
            {
                await EditUser();
                return;
            }

            var response = await authProxy.Register(request);

            if (response.IsSuccessStatusCode)
            {
                message = "Account wurde erfolgreich erstellt.";

                var loginRequest = new LoginRequestDTO
                {
                    Username = request.UserName,
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

        private async Task EditUser()
        {
            UpdateUserRequestDTO updatedUser = new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                ProfilePicture = request.ProfilePicture,
                FileName = request.FileName,
                ContentType = request.ContentType
            };

            await userProxy.UpdateUser(updatedUser);

            await authService.Logout();

            navigationManager.NavigateTo("/login");
        }

        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            const long maxFileSize = 10 * 1024 * 1024; // 10MB

            var selectedFile = e.File;

            using var stream = selectedFile.OpenReadStream(maxFileSize);
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            
            request.ProfilePicture = memoryStream.ToArray();
            request.FileName = selectedFile.Name;
            request.ContentType = selectedFile.ContentType;
        }
    }
}

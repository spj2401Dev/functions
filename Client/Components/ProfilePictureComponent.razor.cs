using Microsoft.AspNetCore.Components;

namespace Functions.Client.Components
{
    public partial class ProfilePictureComponent
    {
        [Parameter] public string UserFirstName { get; set; } = string.Empty;
        [Parameter] public string UserLastName { get; set; } = string.Empty;
        [Parameter] public Guid? UserProfilePictureId { get; set; } = null;
        [Parameter] public int Size { get; set; }
        private bool hasImage = false;
        private bool isLoading = true;

        protected override void OnParametersSet()
        {
            if (UserProfilePictureId.HasValue && UserProfilePictureId.Value != Guid.Empty)
            {
                hasImage = true;
            }
            else
            {
                hasImage = false;
            }

            isLoading = false;
        }
    }
}

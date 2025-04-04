using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Functions.Shared.Interfaces;
using Functions.Shared.DTOs;

namespace Functions.Client.Pages.Event
{
    public partial class EventNew
    {
        [Inject] private IEventsProxy eventProxy { get; set; } = default!;

        [Inject] private NavigationManager navigationManager { get; set; }

        private string newEventName = string.Empty;
        private string newEventLocation = string.Empty;
        private string newEventDescription = string.Empty;
        private DateTime newEventStartDateTime = DateTime.Now;
        private DateTime newEventEndDateTime = DateTime.Now.AddHours(1);
        private string? profilePictureBase64 = null;
        private string? profilePictureFileName = null;
        private string? profilePictureContentType = null;
        private IBrowserFile? selectedFile = null;
        private bool isPublic = false;
        private string uploadStatusMessage = string.Empty;

        private async Task HandleFileSelection(InputFileChangeEventArgs e)
        {
            selectedFile = e.File;
            uploadStatusMessage = $"File selected: {selectedFile.Name}";

            if (selectedFile != null)
            {
                try
                {
                    using var memoryStream = new MemoryStream();
                    await selectedFile.OpenReadStream(maxAllowedSize: 10485760).CopyToAsync(memoryStream); // 10MB max

                    profilePictureBase64 = Convert.ToBase64String(memoryStream.ToArray());
                    profilePictureFileName = selectedFile.Name;
                    profilePictureContentType = selectedFile.ContentType;

                    uploadStatusMessage = "File ready to upload!";
                    StateHasChanged();
                }
                catch (Exception ex)
                {
                    uploadStatusMessage = $"Error processing file: {ex.Message}";
                    profilePictureBase64 = null;
                    profilePictureFileName = null;
                    profilePictureContentType = null;
                }
            }
        }

        private async Task Submit()
        {
            var newEvent = new EventsDTO(
                Guid.Empty, //TODO
                Guid.Empty, //TODO
                newEventName,
                newEventLocation,
                newEventDescription,
                newEventStartDateTime,
                newEventEndDateTime,
                isPublic,
                profilePictureBase64,
                profilePictureFileName,
                profilePictureContentType,
                null
            );

            await eventProxy.PostEventAsync(newEvent);

            ResetForm();
            navigationManager.NavigateTo("/events");
        }

        private async Task Cancel()
        {
            ResetForm();
            navigationManager.NavigateTo("/events");
        }

        private void ResetForm()
        {
            // Reset form
            newEventName = string.Empty;
            newEventLocation = string.Empty;
            newEventDescription = string.Empty;
            newEventStartDateTime = DateTime.Now;
            newEventEndDateTime = DateTime.Now.AddHours(1);
            profilePictureBase64 = null;
            profilePictureFileName = null;
            profilePictureContentType = null;
            selectedFile = null;
            uploadStatusMessage = string.Empty;
            isPublic = false;
        }

    }
}

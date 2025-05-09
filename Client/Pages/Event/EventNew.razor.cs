using Functions.Client.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Functions.Client.Pages.Event
{
    public partial class EventNew
    {
        [Inject] private IEventsProxy eventProxy { get; set; } = default!;

        [Inject] private NavigationManager navigationManager { get; set; }

        [Inject] private AuthService authService { get; set; } = default!;

        [Parameter] public Guid eventId { get; set; }

        private EventsDTO? eventItem;
        private string title = "Create New Event";
        private string newEventName = string.Empty;
        private string newEventLocation = string.Empty;
        private string newEventDescription = string.Empty;
        private DateTime newEventStartDateTime = DateTime.Now.AddHours(1);
        private DateTime newEventEndDateTime = DateTime.Now.AddHours(2);
        private string? profilePictureBase64 = null;
        private string? profilePictureFileName = null;
        private string? profilePictureContentType = null;
        private IBrowserFile? selectedFile = null;
        private bool isPublic = false;
        private string uploadStatusMessage = string.Empty;
        private Guid? userId = Guid.Empty;

        protected override async Task OnParametersSetAsync()
        {
            userId = await authService.GetUserId() ?? Guid.Empty;
            await LoadData();
        }

        private async Task LoadData()
        {
            eventItem = await eventProxy.GetEventById(eventId);
            if (eventItem != null)
            {
                title = "Change Event";
                newEventName = eventItem.Name;
                newEventLocation = eventItem.Location;
                newEventDescription = eventItem.Description;
                newEventStartDateTime = eventItem.StartDateTime;
                newEventEndDateTime = eventItem.EndDateTime;
                isPublic = eventItem.isPublic;
            }

        }
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

        private async Task Save()
        {
            var updatedEvent = new EventsDTO(
                eventItem!.Id,
                userId ?? Guid.Empty,
                newEventName,
                newEventLocation,
                newEventDescription,
                newEventStartDateTime,
                newEventEndDateTime,
                isPublic
            );
            await eventProxy.PutEventAsync(updatedEvent!);
            navigationManager.NavigateTo($"/events/{eventItem.Id}");
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

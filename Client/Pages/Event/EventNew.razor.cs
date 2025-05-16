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

        private EventsDTO? eventItem = new();
        private string title = "Create New Event";
        private string? profilePictureBase64 = null;
        private IBrowserFile? selectedFile = null;
        private string uploadStatusMessage = string.Empty;

        protected override async Task OnParametersSetAsync()
        {
            eventItem.StartDateTime = DateTime.Now.AddDays(1);
            eventItem.EndDateTime = DateTime.Now.AddDays(1).AddHours(2);
            await LoadData();
        }

        private async Task LoadData()
        {
            if (eventId != Guid.Empty)
            {
                eventItem = await eventProxy.GetEventById(eventId);
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

                    eventItem.ProfilePictureBase64 = Convert.ToBase64String(memoryStream.ToArray());
                    eventItem.FileName = selectedFile.Name;
                    eventItem.FileType = selectedFile.ContentType;

                    uploadStatusMessage = "File ready to upload!";
                    StateHasChanged();
                }
                catch (Exception ex)
                {
                    uploadStatusMessage = $"Error processing file: {ex.Message}";
                    eventItem.ProfilePictureBase64 = null;
                    eventItem.FileName = null;
                    eventItem.FileType = null;
                }
            }
        }

        private async Task Save()
        {
            if (eventId != Guid.Empty)
            {
                await UpdateEvent();
            }
            else
            {
                await CreateEvent();
            }
        }

        private async Task CreateEvent()
        {
            await eventProxy.PostEventAsync(eventItem);

            ResetForm();
            navigationManager.NavigateTo("/events");
        }

        private async Task UpdateEvent()
        {
            await eventProxy.PutEventAsync(eventItem!);
            navigationManager.NavigateTo($"/events/{eventItem.Id}");
        }

        private async Task Cancel()
        {
            ResetForm();
            navigationManager.NavigateTo("/events");
        }

        private void ResetForm()
        {
            eventItem = new();
            // Reset form
            eventItem.StartDateTime = DateTime.Now.AddHours(1);
            eventItem.EndDateTime = DateTime.Now.AddHours(2);
            selectedFile = null;
            uploadStatusMessage = string.Empty;
            eventItem.isPublic = false;
        }
    }
}

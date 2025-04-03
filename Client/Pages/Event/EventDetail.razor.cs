using Functions.Client.Services;
using Functions.Shared.DTOs;
using Functions.Shared.DTOs.Messages;
using Functions.Shared.Interfaces;
using Functions.Shared.Interfaces.Messages;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices;

namespace Functions.Client.Pages.Event
{
    public partial class EventDetail
    {
        [Inject] private IEventsProxy eventProxy { get; set; } = default!;
        [Inject] private IMessageProxy messageProxy { get; set; } = default!;
        [Inject] private NavigationManager navigationManager { get; set; } = default!;

        [Parameter] 
        public Guid eventid { get; set; }

        [Inject] private AuthService authService { get; set; } = default!;
        [Parameter] public Guid eventId { get; set; }

        private EventsDTO? eventItem;
        private List<MessageDTO> messages = new();
        private AnnouncementRequestDTO announcemenRequest = new();
        private Guid? userId = Guid.Empty;
        private bool isAuthenticated = false;

        protected override async Task OnParametersSetAsync()
        {
            userId = await authService.GetUserId() ?? Guid.Empty;
            isAuthenticated = await authService.IsAuthenticated();
            await LoadData();
            await LoadMessages();
        }

        private async Task LoadData()
        {
            eventItem = await eventProxy.GetEventsbyIdAsync(eventId);
        }

        private void ReturnToMainPage()
        {
            navigationManager.NavigateTo("/events");
        }

        private async Task CreateAnnouncement()
        {
            announcemenRequest.EventId = eventItem?.Id;

            await messageProxy.PostAnnouncement(announcemenRequest);

            MessageDTO newMessageDTO = new MessageDTO
            {
                Text = announcemenRequest.Message,
                MessageDate = DateTime.Now,
            };

            messages.Add(newMessageDTO);
        }

        private async Task LoadMessages()
        {
            if (eventItem != null)
            {
                messages = await messageProxy.GetMessagesForEvent(eventItem.Id);
            }
        }
    }
}

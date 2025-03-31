using Functions.Shared.DTOs;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages
{
    public partial class Index
    {
        [Inject] private IEventsProxy eventProxy { get; set; } = default!;
        private string newEventName = string.Empty;
        private string newEventLocation = string.Empty;
        private string newEventDescription = string.Empty;
        private DateTime newEventStartDateTime;
        private DateTime newEventEndDateTime;

        private List<EventsDTO> events = new List<EventsDTO>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            events = await eventProxy.GetEventsAsync();
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
                null
            );

            await eventProxy.PostEventAsync(newEvent);
            await LoadData();
        }
    }
}
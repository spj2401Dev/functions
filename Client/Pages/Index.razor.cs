using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages
{
    public partial class Index
    {
        [Inject] IEventsProxy eventsProxy { get; set; } = default!;

        private List<EventMasterPageDTO> userEvents { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            userEvents = await eventsProxy.GetAllEventsByUserAsync();
        }
    }
}

using Functions.Shared.DTOs;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages.Event
{
    public partial class EventDetail
    {
        [Inject] private IEventsProxy eventProxy { get; set; } = default!;
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Parameter] 
        public Guid eventid { get; set; }

        private EventsDTO? eventItem;

        protected override async Task OnParametersSetAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            eventItem = await eventProxy.GetEventsbyIdAsync(eventid);
        }

        private async Task ReturnToMainPage()
        {
            navigationManager.NavigateTo("/events");
        }

    }
}

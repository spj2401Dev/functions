using Functions.Shared.DTOs;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages.Event
{
    public partial class EventList
    {
        [Inject] private IEventsProxy eventProxy { get; set; } = default!;

        [Inject] private NavigationManager navigationManager { get; set; }

        private List<EventsDTO> events = new List<EventsDTO>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            events = await eventProxy.GetEventsAsync();
        }
		private void OnDetailButtonClick(Guid eventID)
		{
            navigationManager.NavigateTo($"/events/{eventID}");
		}
		private void OnNewEventButtonClick()
		{
            navigationManager.NavigateTo("/events/new");
		}
    }
}
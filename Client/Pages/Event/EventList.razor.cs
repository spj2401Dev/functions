using Functions.Client.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages.Event
{
    public partial class EventList
    {
        [Inject] private IEventsProxy eventProxy { get; set; } = default!;

        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private AuthService authService { get; set; } = default!;

        private List<EventMasterPageDTO> events = new List<EventMasterPageDTO>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            events = await eventProxy.GetEventsAsync();
        }

        private void OnDetailButtonClick(Guid eventId)
        {

            navigationManager.NavigateTo($"/events/{eventId}");
        }

        private async Task NewEvent()
        {
            if (await authService.IsAuthenticated())
            {
                navigationManager.NavigateTo($"/events/new");
            }
            else
            {
                navigationManager.NavigateTo($"/login/events/new");
            }
        }
    }
}
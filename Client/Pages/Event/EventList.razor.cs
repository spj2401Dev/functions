using Functions.Client.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages.Event
{
    public partial class EventList
    {
        [Inject] private IEventsProxy eventProxy { get; set; } = default!;

        [Inject] private NavigationManager navigationManager { get; set; }

        [Inject] private AuthService authService { get; set; }


        private bool isAuthenticated;
        private List<EventMasterPageDTO> events = new List<EventMasterPageDTO>();

        protected override async Task OnInitializedAsync()
        {
            isAuthenticated = await authService.IsAuthenticated();
            await LoadData();
        }

        private async Task LoadData()
        {
            events = await eventProxy.GetEventsAsync();
        }

        private async Task OnDetailButtonClick(Guid eventID)
        {
            if (await authService.IsAuthenticated())
            {
                navigationManager.NavigateTo($"/events/{eventID}");
            }
            else
            {
                navigationManager.NavigateTo($"/login/events/{eventID}");
            }
        }
    }
}
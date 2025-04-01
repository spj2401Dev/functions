using Functions.Shared.DTOs;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Functions.Client.Pages
{
    public partial class Event
    {
        [Inject] private IEventsProxy eventProxy { get; set; } = default!;


        private Guid eventid;

        private EventsDTO eventItem;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            //eventItem = await eventProxy.get();
        }

    }
}

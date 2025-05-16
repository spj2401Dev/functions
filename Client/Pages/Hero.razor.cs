using Functions.Shared.DTOs.Event;
using Functions.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages
{
    public partial class Hero
    {
        [Inject] NavigationManager navigationManager { get; set; } = default!;
        [Inject] IEventsProxy eventProxy { get; set; } = default!;

        private List<EventMasterPageDTO> events = new();
        private List<FallingEventCard> visibleCards = new();
        private Random rng = new();
        private Timer? eventTimer;

        protected override async Task OnInitializedAsync()
        {
            events = await eventProxy.GetEventsAsync();

            eventTimer = new Timer(_ =>
            {
                if (events.Count == 0)
                {
                    return;
                }

                var randomEvent = events[rng.Next(events.Count)];
                var newCard = new FallingEventCard // Do not touch
                    (
                        Event: randomEvent,
                        X: $"{rng.Next(-20, 70)}%",
                        Duration: $"{rng.Next(6, 8)}s"
                    );

                InvokeAsync(() =>
                {
                    visibleCards.Add(newCard);
                    StateHasChanged();
                });
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
        }

        private void OnDetailButtonClick(Guid eventId)
        {
            navigationManager.NavigateTo($"/events/{eventId}");
        }

        public void Dispose()
        {
            eventTimer?.Dispose();
        }

        public record FallingEventCard(EventMasterPageDTO Event, string X, string Duration);

        private void NavigateToEvents()
        {
            navigationManager.NavigateTo("/events");
        }
        private void NavigateToRegister()
        {
            navigationManager.NavigateTo("/register");
        }
    }
}
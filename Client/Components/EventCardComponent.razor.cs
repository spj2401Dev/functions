using Functions.Client.Services;
using Functions.Shared.DTOs.Event;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Components
{
    public partial class EventCardComponent
    {
        [Parameter] public EventMasterPageDTO Event { get; set; } = default!;
        [Parameter] public EventCallback<Guid> OnEventClicked { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private AuthService authService { get; set; } = default!;
    }
}

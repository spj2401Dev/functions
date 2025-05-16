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

        string FormatEventDate(DateTime start, DateTime end)
        {
            var now = DateTime.Now;
            bool sameYear = start.Year == now.Year && end.Year == now.Year;
            bool sameDay = start.Date == end.Date;

            string startFormat;
            string endFormat;

            if (!sameYear)
            {
                startFormat = start.ToString("dd.MM.yy HH:mm", new System.Globalization.CultureInfo("de-DE"));
                endFormat = end.ToString("dd.MM.yy HH:mm", new System.Globalization.CultureInfo("de-DE"));
            }
            else if (sameDay)
            {
                startFormat = start.ToString("dd.MM. HH:mm", new System.Globalization.CultureInfo("de-DE"));
                endFormat = end.ToString("HH:mm", new System.Globalization.CultureInfo("de-DE"));
            }
            else
            {
                startFormat = start.ToString("dd.MM HH:mm", new System.Globalization.CultureInfo("de-DE"));
                endFormat = end.ToString("dd.MM HH:mm", new System.Globalization.CultureInfo("de-DE"));
            }

            return $"{startFormat} - {endFormat}";
        }

    }
}

using Microsoft.AspNetCore.Components;

namespace Functions.Client.Components
{
    public partial class LandingPage
    {
        [Parameter] public EventCallback SwitchPage { get; set; }
        private bool fadeout = false;


        private async Task SwitchPageButton()
        {
            await SwitchPage.InvokeAsync();
        }
    }
}

using Functions.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Forms
{
    public partial class PersonForm
    {
        [Inject] HttpClient Http { get; set; }
        private PersonDTO person = new();
        private bool isSubmitting = false;

        private async Task OnValidSubmit()
        {

        }

    }
}
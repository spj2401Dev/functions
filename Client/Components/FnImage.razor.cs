using Microsoft.AspNetCore.Components;

namespace Functions.Client.Components
{
    public partial class FnImage
    {
        [Parameter] public required Guid FileId { get; set; }
        [Parameter] public string? CssClass { get; set; }
        [Parameter] public string? CssStyle { get; set; }
        [Parameter] public bool LazyLoading { get; set; } = true;
        [Inject] IConfiguration configuration { get; set; } = default!;

        private string? lazyLoading => LazyLoading ? "lazy" : "eager";
        private string srcUrl;

        protected override async Task OnParametersSetAsync()
        {
            srcUrl = configuration["ApiClient:BaseUrl"];
        }
    }
}

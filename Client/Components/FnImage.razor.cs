using Microsoft.AspNetCore.Components;

namespace Functions.Client.Components
{
    public partial class FnImage
    {
        [Parameter] public required Guid FileId { get; set; }
        [Parameter] public string? CssClass { get; set; }
        [Parameter] public string? CssStyle { get; set; } = "max-width: 100%";
        [Parameter] public bool LazyLoading { get; set; } = true;
        [Inject] IConfiguration configuration { get; set; } = default!;
        [Inject] HttpClient httpClient { get; set; } = default!;

        private string? lazyLoading => LazyLoading ? "lazy" : "eager";
        private string srcUrl = string.Empty;

        protected override void OnParametersSet()
        {
            srcUrl = httpClient.BaseAddress + "api/file/downloadFile/?file=" + FileId;
        }
    }
}

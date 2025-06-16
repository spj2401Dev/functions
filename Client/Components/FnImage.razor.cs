using Microsoft.AspNetCore.Components;

namespace Functions.Client.Components
{
    public partial class FnImage
    {
        [Parameter] public Guid? FileId { get; set; }
        [Parameter] public string? CssClass { get; set; }
        [Parameter] public string? CssStyle { get; set; } = "max-width: 100%";
        [Parameter] public bool LazyLoading { get; set; } = true;
        [Parameter] public string? ImageUrl { get; set; }
        [Inject] HttpClient httpClient { get; set; } = default!;

        private string? lazyLoading => LazyLoading ? "lazy" : "eager";
        private string srcUrl = string.Empty;

        protected override void OnParametersSet()
        {
            if (FileId != null && FileId != Guid.Empty)
            {
                srcUrl = httpClient.BaseAddress + "api/file/downloadFile/?file=" + FileId;
                return;
            }

            if (ImageUrl != null && ImageUrl != string.Empty)
            {
                srcUrl = ImageUrl;
                return;
            }

            throw new ArgumentException("FnImage components requires either a FileId or a ImageUrl");
        }
    }
}

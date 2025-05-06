using Functions.Shared.DTOs.Messages;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Components.Comments
{
    public partial class CommentComponent
    {
        [Parameter]
        public MessageDTO Comment { get; set; } = default!;

        [Parameter]
        public List<MessageDTO> AllComments { get; set; } = new();

        [Parameter]
        public EventCallback<(Guid ParentId, string Text)> OnPostReply { get; set; }

        [Parameter]
        public bool IsAuthenticated { get; set; }

        private bool IsReplyBoxOpen { get; set; } = false;
        private string ReplyText { get; set; } = string.Empty;

        private List<MessageDTO> ChildComments => AllComments
            .Where(x => x.ParentId == Comment.Id)
            .ToList();

        private void ToggleReplyBox()
        {
            IsReplyBoxOpen = !IsReplyBoxOpen;
            if (!IsReplyBoxOpen)
            {
                ReplyText = string.Empty;
            }
        }

        private async Task PostReply()
        {
            if (string.IsNullOrEmpty(ReplyText))
            {
                return;
            }

            await OnPostReply.InvokeAsync((Comment.Id, ReplyText));
            ReplyText = string.Empty;
            IsReplyBoxOpen = false;
        }
    }
}
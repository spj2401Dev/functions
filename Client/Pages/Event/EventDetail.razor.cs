using Functions.Client.Services;
using Functions.Shared.DTOs.Event;
using Functions.Shared.DTOs.Messages;
using Functions.Shared.DTOs.Participation;
using Functions.Shared.Enum;
using Functions.Shared.Interfaces;
using Functions.Shared.Interfaces.Messages;
using Functions.Shared.Interfaces.Participation;
using Microsoft.AspNetCore.Components;

namespace Functions.Client.Pages.Event
{
    public partial class EventDetail
    {
        [Inject] private IEventsProxy eventProxy { get; set; } = default!;
        [Inject] private IMessageProxy messageProxy { get; set; } = default!;
        [Inject] private IParticipationProxy participationProxy { get; set; } = default!;
        [Inject] private NavigationManager navigationManager { get; set; } = default!;
        [Inject] private AuthService authService { get; set; } = default!;
        [Parameter] public Guid eventId { get; set; }

        private EventsDTO? eventItem;
        private List<MessageDTO> messages = new();
        private GetParticipationResponseDTO participations = new();
        private AnnouncementRequestDTO announcemenRequest = new();
        private Guid? userId = Guid.Empty;
        private bool isAuthenticated = false;
        private string comment = string.Empty;
        private Dictionary<Guid, bool> replyBoxOpen = new();
        private Dictionary<Guid, string> replyTexts = new();

        protected override async Task OnParametersSetAsync()
        {
            userId = await authService.GetUserId() ?? Guid.Empty;
            isAuthenticated = await authService.IsAuthenticated();
            await LoadData();
            await LoadParticipation();
            await LoadMessages();
        }

        private async Task LoadData()
        {
            eventItem = await eventProxy.GetEventsbyIdAsync(eventId);
        }

        private void ReturnToMainPage()
        {
            navigationManager.NavigateTo("/events");
        }

        private async Task CreateAnnouncement()
        {
            announcemenRequest.EventId = eventItem?.Id;

            await messageProxy.PostAnnouncement(announcemenRequest);

            MessageDTO newMessageDTO = new MessageDTO
            {
                Text = announcemenRequest.Message,
                MessageDate = DateTime.Now,
            };

            messages.Add(newMessageDTO);
        }

        private async Task LoadMessages()
        {
            if (eventItem != null)
            {
                messages = await messageProxy.GetMessagesForEvent(eventItem.Id);
            }
        }

        private async Task LoadParticipation()
        {
            if (eventItem?.Id == Guid.Empty || eventItem == null)
            {
                return;
            }

            participations = await participationProxy.GetParticipaton(eventItem.Id);
        }

        private async Task PostParticipation(ParticipationStatus status)
        {
            if (eventItem?.Id == Guid.Empty || eventItem == null)
            {
                return;
            }

            if (!await authService.IsAuthenticated())
            {
                navigationManager.NavigateTo($"/login/events/{eventItem.Id}");
            }

            var requestDTO = new PostParticipationDTO
            {
                EventId = eventItem.Id,
                Type = status
            };

            await participationProxy.PostParticipation(requestDTO);
            await LoadParticipation();
        }

        private async Task PostComment(Guid? ParentId = null)
        {
            if (comment == string.Empty || comment == null)
            {
                return;
            }

            if (!await authService.IsAuthenticated())
            {
                navigationManager.NavigateTo($"/login/events/{eventItem.Id}");
            }

            var requestDTO = new CommentRequestDTO
            {
                EventId = eventItem?.Id,
                Comment = comment,
                ParentId = ParentId
            };

            await messageProxy.PostMessage(requestDTO);
            await LoadMessages();
        }

        private void ToggleReplyBox(Guid messageId)
        {
            if (!replyBoxOpen.ContainsKey(messageId))
            {
                replyBoxOpen[messageId] = true;
                replyTexts[messageId] = string.Empty;
            }
            else
            {
                replyBoxOpen[messageId] = !replyBoxOpen[messageId];
            }
        }

        private async Task PostReply(Guid parentId)
        {
            if (!replyTexts.ContainsKey(parentId) || string.IsNullOrEmpty(replyTexts[parentId]))
            {
                return;
            }

            if (!await authService.IsAuthenticated())
            {
                navigationManager.NavigateTo($"/login/events/{eventItem?.Id}");
                return;
            }

            var requestDTO = new CommentRequestDTO
            {
                EventId = eventItem?.Id,
                Comment = replyTexts[parentId],
                ParentId = parentId
            };

            await messageProxy.PostMessage(requestDTO);
            
            // Clear the reply text and close the reply box
            replyTexts[parentId] = string.Empty;
            replyBoxOpen[parentId] = false;
            
            await LoadMessages();
            comment = string.Empty; // Clear the main comment box too
        }
    }
}

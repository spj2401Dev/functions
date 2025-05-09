using Functions.Shared.DTOs.Event;
using Functions.Shared.DTOs.Users;

namespace Functions.Shared.DTOs.Messages
{
    public class HomePageAnnouncementResponseDTO
    {
        //public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime MessageDate { get; set; }
        public SafeUserResponseDTO Creator { get; set; }
        public EventMasterPageDTO Event { get; set; }
    }
}

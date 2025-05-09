using Functions.Shared.DTOs.Messages;

namespace Functions.Shared.DTOs.Event
{
    public class HomePageResponseDTO
    {
        public List<EventMasterPageDTO> Events { get; set; } = new();
        public List<HomePageAnnouncementResponseDTO> Announcements { get; set; } = new();
    }
}

namespace Functions.Shared.DTOs.Messages
{
    public class AnnouncementRequestDTO
    {
        public string Message { get; set; } = string.Empty;
        public Guid? EventId { get; set; }
    }
}

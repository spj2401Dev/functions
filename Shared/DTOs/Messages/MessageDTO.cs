namespace Functions.Shared.DTOs.Messages
{
    public class MessageDTO
    {
        public string Text { get; set; } = string.Empty;
        public DateTime MessageDate { get; set; }
        // Creator
        public Guid? ParentId { get; set; }
    }
}

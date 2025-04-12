namespace Functions.Shared.DTOs.Event
{
    public class EventMasterPageDTO
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? ImageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string Location { get; set; }
        public string? Description { get; set; }
    }
}

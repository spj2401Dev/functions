using System.ComponentModel.DataAnnotations;

namespace Functions.Server.Model
{
    public class Events
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Host { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Guid? PictureId { get; set; }
    }
}

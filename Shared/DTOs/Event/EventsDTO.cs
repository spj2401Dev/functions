using System.ComponentModel.DataAnnotations;
using Functions.Shared.Annotations;

namespace Functions.Shared.DTOs.Event
{
    public class EventsDTO
    {
        public Guid Id { get; set; }
        public Guid HostId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Location { get; set; } = string.Empty;

        [MinLength(1)]
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DateMustBeBefore(nameof(EndDateTime))]
        [DataType(DataType.DateTime)]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DateMustBeAfter(nameof(StartDateTime))]
        [DataType(DataType.DateTime)]
        public DateTime EndDateTime { get; set; }

        [Required]
        public bool isPublic { get; set; } = false;

        public string? ProfilePictureBase64 { get; set; } = null;
        public string? FileName { get; set; } = null;
        public string? FileType { get; set; } = null;
        public Guid? FileId { get; set; } = null;
    }
}

using System.ComponentModel.DataAnnotations;
using Functions.Shared.Annotations;

namespace Functions.Shared.DTOs.Event
{
    public class EventsDTO
    {
        Guid Id { get; set; }
        Guid HostId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        string Location { get; set; } = string.Empty;

        [MinLength(1)]
        [MaxLength(255)]
        string Description { get; set; } = string.Empty;

        [Required]
        [DateMustBeBefore(nameof(EndDateTime))]
        [DataType(DataType.DateTime)]
        DateTime StartDateTime { get; set; }

        [Required]
        [DateMustBeAfter(nameof(StartDateTime))]
        [DataType(DataType.DateTime)]
        DateTime EndDateTime { get; set; }

        [Required]
        bool isPublic { get; set; } = false;

        string? ProfilePictureBase64 { get; set; } = null;
        string? FileName { get; set; } = null;
        string? FileType { get; set; } = null;
        Guid? FileId { get; set; } = null;
    }
}

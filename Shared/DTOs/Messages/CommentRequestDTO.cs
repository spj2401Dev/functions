using System.ComponentModel.DataAnnotations;

namespace Functions.Shared.DTOs.Messages
{
    public class CommentRequestDTO
    {
        public Guid? EventId { get; set; }
        public Guid? ParentId { get; set; }
        [Required]
        [MaxLength(255)]
        public required string Comment { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Functions.Shared.DTOs.Users
{
    public class UpdateUserRequestDTO
    {
        [Required]
        public Guid UserId { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string? NewFirstName { get; set; } = null;

        [MinLength(3)]
        [MaxLength(50)]
        public string? NewLastName { get; set; } = null;

        [EmailAddress]
        public string? NewEmail { get; set; } = null;

        [MinLength(6)]
        [MaxLength(20)]
        public string? Password { get; set; } = null;
    }
}

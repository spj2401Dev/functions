using System.ComponentModel.DataAnnotations;

namespace Functions.Shared.DTOs.Users
{
    public class UpdateUserRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [MinLength(6)]
        [MaxLength(20)]
        public string? Password { get; set; } = null;
    }
}

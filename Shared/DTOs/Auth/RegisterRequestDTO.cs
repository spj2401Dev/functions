using System.ComponentModel.DataAnnotations;

namespace Functions.Shared.DTOs.Auth
{
    public class RegisterRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        public string? ProfilePictureBase64 { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
    }
}

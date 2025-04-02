using System.ComponentModel.DataAnnotations;

namespace Functions.Shared.DTOs.Auth
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

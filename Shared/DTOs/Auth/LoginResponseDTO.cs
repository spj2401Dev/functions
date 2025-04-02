namespace Functions.Shared.DTOs.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public DateTime Expiration { get; set; }
    }
}

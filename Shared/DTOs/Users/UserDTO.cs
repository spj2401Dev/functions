namespace Functions.Shared.DTOs.Users
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Notificaions { get; set; } = false;
        public Guid? profilePictureId { get; set; } = null;
    }
}

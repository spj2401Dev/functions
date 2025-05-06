namespace Functions.Shared.DTOs.Users
{
    public class SafeUserResponseDTO
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";
    }
}

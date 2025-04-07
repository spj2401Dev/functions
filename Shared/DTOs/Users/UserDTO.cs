namespace Functions.Shared.DTOs.Users
{
    public record UserDTO
    (
        Guid UserId,
        string Username,
        string Firstname,
        string Lastname, 
        string Email,
        bool Notificaions,
        Guid? profilePictureID
    );
}

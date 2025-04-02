namespace Functions.Shared.DTOs.Users
{
    public record UserDTO
    (
        Guid Id,
        string Username ,
        string Firstname,
        string Lastname ,
        string Password ,
        string Email ,
        bool Notificaions ,
        string? ProfilePictureBase64 = null ,
        string? FileName = null,
        string? FileType = null
    );
}

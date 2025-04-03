namespace Functions.Shared.DTOs.Users
{
    public record UserDTO
    (
        Guid Id,
        string Username ,
        string Firstname,
        string Lastname, 
        string Email,
        bool Notificaions
    );
}

namespace Functions.Shared.DTOs
{
    public record EventsDTO
    (
        Guid Id,
        Guid Host,
        string Name,
        string Location,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        string? ProfilePictureBase64
    );
}

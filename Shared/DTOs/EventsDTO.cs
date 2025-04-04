namespace Functions.Shared.DTOs
{
    public record EventsDTO
    (
        Guid Id,
        Guid HostId,
        string Name,
        string Location,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        bool isPublic = false,
        string? ProfilePictureBase64 = null,
        string? FileName = null,
        string? FileType = null,
        Guid? FileId = null
    );
}

using Functions.Shared.Enum;

namespace Functions.Shared.DTOs.Participation
{
    public class GetParticipationResponseDTO
    {
        public IEnumerable<GetParticipationUsersResponseDTO> Users { get; set; } = new List<GetParticipationUsersResponseDTO>();
        public ParticipationStatus? Status { get; set; } = null;
    }

    public class GetParticipationUsersResponseDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FullName => $"{FirstName} {LastName}";
        public Guid? UserProfilePictureFileId { get; set; }
        public ParticipationStatus Status { get; set; }
    }
}

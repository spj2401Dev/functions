using Functions.Shared.Enum;

namespace Functions.Shared.DTOs.Participation
{
    public class PostParticipationDTO
    {
        public Guid EventId { get; set; }
        public ParticipationStatus Type { get; set; }
    }
}

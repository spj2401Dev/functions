using Functions.Shared.DTOs.Participation;

namespace Functions.Shared.Interfaces.Participation
{
    public interface IParticipationProxy
    {
        Task PostParticipation(PostParticipationDTO request);
        Task<GetParticipationResponseDTO> GetParticipaton(Guid EventId);
    }
}

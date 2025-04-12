using Functions.Shared.Enum;

namespace Functions.Server.Interfaces.Participation
{
    public interface IPostParticipationUseCase
    {
        Task Handle(Guid userId, Guid eventId, ParticipationStatus Type);
    }
}

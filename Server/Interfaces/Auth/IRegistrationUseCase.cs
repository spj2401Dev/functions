using Functions.Shared.DTOs.Auth;

namespace Functions.Server.Interfaces.Auth
{
    public interface IRegistrationUseCase
    {
        Task Handle(RegisterRequestDTO request);
    }
}

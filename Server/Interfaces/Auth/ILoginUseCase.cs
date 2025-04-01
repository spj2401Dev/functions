using Functions.Shared.DTOs.Auth;

namespace Functions.Server.Interfaces.Auth
{
    public interface ILoginUseCase
    {
        Task<LoginResponseDTO> Handle(LoginRequestDTO request);
    }
}

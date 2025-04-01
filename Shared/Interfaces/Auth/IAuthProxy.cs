using Functions.Shared.DTOs.Auth;

namespace Functions.Shared.Interfaces.Auth
{
    public interface IAuthProxy
    {
        Task<HttpResponseMessage> Register(RegisterRequestDTO request);
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
    }
}

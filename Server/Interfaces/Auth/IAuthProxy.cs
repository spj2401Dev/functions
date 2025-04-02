using Functions.Shared.DTOs.Auth;

namespace Functions.Server.Interfaces.Auth
{
    public interface IAuthProxy
    {
        Task<HttpResponseMessage> Register(RegisterRequestDTO request);
    }
}

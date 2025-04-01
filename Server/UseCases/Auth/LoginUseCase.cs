using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Auth;
using Functions.Server.Model;
using Functions.Server.Services.Auth;
using Functions.Shared.DTOs.Auth;

namespace Functions.Server.UseCases.Auth
{
    public class LoginUseCase(IRepository<User> userRepository, JwtService jwtService) : ILoginUseCase
    {
        public async Task<LoginResponseDTO> Handle(LoginRequestDTO request)
        {
            // Find user by username
            var user = await userRepository.GetAllAsync().ContinueWith(t => t.Result.FirstOrDefault(u => u.Username == request.Username));

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            if (user.Password != request.Password)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            return jwtService.GenerateToken(user.Username, user.Id.ToString());
        }
    }
}

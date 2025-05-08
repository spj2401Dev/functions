using Functions.Shared.DTOs.Auth;
using Functions.Shared.DTOs.Users;

namespace Functions.Server.Interfaces.Users
{
    public interface IUpdateUserUseCase
    {
        Task Handle(UpdateUserRequestDTO updateUserRequestDTO, Guid userId);
    }
}

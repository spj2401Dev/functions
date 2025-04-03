using Functions.Shared.DTOs.Users;

namespace Functions.Server.Interfaces.Users
{
    public interface IGetUserByIdUseCase
    {
        Task<UserDTO> Handle(Guid id);
    }
}

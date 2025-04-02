using Functions.Shared.DTOs.Users;

namespace Functions.Server.Interfaces.Users
{
    public interface IGetAllUserUseCase
    {
        Task<List<UserDTO>> Handle();
    }
}

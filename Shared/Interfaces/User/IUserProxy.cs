using Functions.Shared.DTOs.Users;

namespace Functions.Shared.Interfaces.User
{
    public interface IUserProxy
    {
        Task<List<UserDTO>> GetAllUsers();

        Task<UserDTO> GetUserById(Guid id);
    }
}

using Functions.Shared.DTOs.Users;

namespace Functions.Shared.Interfaces.User
{
    public interface IUserProxy
    {
        Task<UserDTO> GetUser();

        Task<HttpResponseMessage> UpdateUser(UpdateUserRequestDTO updateUserRequestDTO);
    }
}

using Functions.Shared.DTOs.Users;

namespace Functions.Shared.Interfaces.User
{
    public interface IUserProxy
    {
        Task<UserDTO> GetUserById(Guid id);

        Task<HttpResponseMessage> UpdaetUser(UpdateUserRequestDTO updateUserRequestDTO);
    }
}

using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Users;
using Functions.Server.Model;
using Functions.Shared.DTOs.Users;

namespace Functions.Server.UseCases.Users
{

    public class GetUserByIdUseCase(IRepository<User> userRepository) : IGetUserByIdUseCase
    {
        public async Task<UserDTO> Handle(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }


            return new UserDTO
            {
                UserId = user.Id,
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Notificaions = user.Notifications,
                profilePictureId = user.ProfilePictureId
            };
        }
    }
}

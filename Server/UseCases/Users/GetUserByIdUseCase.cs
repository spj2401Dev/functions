using Azure.Core;
using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Users;
using Functions.Server.Model;
using Functions.Server.Repsitorys;
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

            return new UserDTO(user.Id,
                user.Username,
                user.Firstname,
                user.Lastname,
                user.Email,
                user.Notifications,
                user.ProfilePictureId
                );
        }
    }
}

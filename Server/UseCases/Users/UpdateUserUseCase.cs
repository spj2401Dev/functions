using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Users;
using Functions.Server.Model;
using Functions.Shared.DTOs.Users;

namespace Functions.Server.UseCases.Users
{
    public class UpdateUserUseCase(IRepository<User> userRepository) : IUpdateUserUseCase
    {
        public async Task Handle(UpdateUserRequestDTO updateUserRequestDTO, Guid userId)
        {

            var user = await userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            // username cannot be changed
            user.Firstname = updateUserRequestDTO.FirstName;
            user.Lastname = updateUserRequestDTO.LastName;
            user.Email = updateUserRequestDTO.Email;
            if (updateUserRequestDTO.Password != string.Empty && updateUserRequestDTO.Password != null)
            {
                user.Password = updateUserRequestDTO.Password;
            }

            await userRepository.UpdateAsync(user);
        }
    }
}

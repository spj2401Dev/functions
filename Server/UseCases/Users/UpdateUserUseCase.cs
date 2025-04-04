using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Users;
using Functions.Server.Model;
using Functions.Shared.DTOs.Users;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Functions.Server.UseCases.Users
{
    public class UpdateUserUseCase(IRepository<User> userRepository) : IUpdateUserUseCase
    {
        public async Task Handle(UpdateUserRequestDTO updateUserRequestDTO)
        {
            var user = await userRepository.GetByIdAsync(updateUserRequestDTO.UserId);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(updateUserRequestDTO.UserId));
            }

            user.Firstname = updateUserRequestDTO.NewFirstName ?? user.Firstname;

            user.Lastname = updateUserRequestDTO.NewLastName ?? user.Lastname;

            user.Email = updateUserRequestDTO.NewEmail ?? user.Email;



            await userRepository.UpdateAsync(user);

        }
    }
}

using Azure.Core;
using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Users;
using Functions.Server.Model;
using Functions.Server.Repsitorys;
using Functions.Shared.DTOs.Users;

namespace Functions.Server.UseCases.Users
{

    public class GetUserByIdUseCase(IRepository<User> userRepository, IRepository<Files> fileRepository) : IGetUserByIdUseCase
    {
        public async Task<UserDTO> Handle(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            string? base64Image = null;
            if (user.ProfilePictureId.HasValue)
            {
                var file = await fileRepository.GetByIdAsync(user.ProfilePictureId.Value);
                if (file != null && file.FileContent != null)
                {
                    base64Image = file.FileContent.Base64Content;
                }
            }

            return new UserDTO(user.Id,
                user.Username,
                user.Firstname,
                user.Lastname,
                user.Password,
                user.Email,
                user.Notifications,
                base64Image,
                null,
                null
                );
        }
    }
}

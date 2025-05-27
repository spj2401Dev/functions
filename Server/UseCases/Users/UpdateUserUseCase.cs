using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Users;
using Functions.Server.Model;
using Functions.Server.Services.File;
using Functions.Shared.DTOs.Users;

namespace Functions.Server.UseCases.Users
{
    public class UpdateUserUseCase(IRepository<User> userRepository,
                                   FilesService filesService) : IUpdateUserUseCase
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

            if (updateUserRequestDTO.ProfilePicture != null &&
                !string.IsNullOrEmpty(updateUserRequestDTO.FileName) &&
                !string.IsNullOrEmpty(updateUserRequestDTO.ContentType))
            {
                var profilePictureBase64 = updateUserRequestDTO.ProfilePicture != null
                    ? Convert.ToBase64String(updateUserRequestDTO.ProfilePicture)
                    : null;

                var fileId = await filesService.SaveFileAsync(profilePictureBase64, updateUserRequestDTO.FileName, updateUserRequestDTO.ContentType);
                user.ProfilePictureId = fileId;
            }

            await userRepository.UpdateAsync(user);
        }
    }
}

using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Auth;
using Functions.Server.Model;
using Functions.Server.Services.File;
using Functions.Shared.DTOs.Auth;

namespace Functions.Server.UseCases
{
    public class RegistrationUseCase(IRepository<User> userRepository, FilesService filesService) : IRegistrationUseCase
    {
        public async Task Handle(RegisterRequestDTO request)
        {
            var existingUsers = await userRepository.GetAllAsync();

            if (existingUsers.Any(u => u.Username == request.UserName))
            {
                throw new Exception("Username already exists");
            }

            if (existingUsers.Any(u => u.Email == request.Email))
            {
                throw new Exception("Email is already in use");
            }

            var user = new User
            {
                Username = request.UserName,
                Email = request.Email,
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Password = request.Password,
            };

            if (request.ProfilePicture != null &&
                !string.IsNullOrEmpty(request.FileName) &&
                !string.IsNullOrEmpty(request.ContentType))
            {
                var profilePictureBase64 = request.ProfilePicture != null
                    ? Convert.ToBase64String(request.ProfilePicture)
                    : null;

                var fileid = await filesService.SaveFileAsync(profilePictureBase64, request.FileName, request.ContentType);
                user.ProfilePictureId = fileid;
            }

            await userRepository.AddAsync(user);
        }
    }
}

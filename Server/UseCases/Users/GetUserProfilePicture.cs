using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Users;
using Functions.Server.Model;
using Functions.Server.Repsitorys;
using Functions.Shared.DTOs.Users;
using Microsoft.Extensions.Logging;

namespace Functions.Server.UseCases.Users
{
    public class GetUserProfilePicture(IRepository<User> userRepository, IRepository<Files> fileRepository) : IGetUserProfilePicture
    {
        public async Task<ProfilePictureDTO> Handle(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var stream = new MemoryStream();
            if (user.ProfilePictureId.HasValue)
            {
                var file = await fileRepository.GetByIdAsync(user.ProfilePictureId.Value);
                var writer = new StreamWriter(stream);
                writer.Write(file.FileContent?.Base64Content);
                writer.Flush();
                stream.Position = 0;
            }

            return new ProfilePictureDTO(stream);
        }
    }
}

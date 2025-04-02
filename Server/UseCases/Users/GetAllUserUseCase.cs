using Functions.Server.Interfaces;
using Functions.Server.Interfaces.Users;
using Functions.Server.Model;
using Functions.Server.Repsitorys;
using Functions.Shared.DTOs.Users;
using Microsoft.Extensions.Logging;

namespace Functions.Server.UseCases.Users
{
    public class GetAllUserUseCase(IRepository<User> userRepository, IRepository<Files> fileRepository) : IGetAllUserUseCase
    {
        public async Task<List<UserDTO>> Handle()
        {
            var allusers = await userRepository.GetAllAsync();
            var result = new List<UserDTO>();

            foreach (var e in allusers)
            {
                string? base64Image = null;
                if (e.ProfilePictureId.HasValue)
                {
                    var file = await fileRepository.GetByIdAsync(e.ProfilePictureId.Value);
                    if (file != null && file.FileContent != null)
                    {
                        base64Image = file.FileContent.Base64Content;
                    }
                }

                result.Add(new UserDTO(
                    e.Id,
                    e.Username,
                    e.Firstname,
                    e.Lastname,
                    e.Password,
                    e.Email,
                    e.Notifications,
                    base64Image,
                    null,
                    null
                ));
            }


            return result;
        }
    }
}

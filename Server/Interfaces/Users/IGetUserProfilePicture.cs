using Functions.Shared.DTOs.Users;

namespace Functions.Server.Interfaces.Users
{
    public interface IGetUserProfilePicture
    {
        Task<ProfilePictureDTO> Handle(Guid id);
    }
}

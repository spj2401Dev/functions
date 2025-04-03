using Azure.Core;
using System.Net;
using Functions.Server.Interfaces.Users;
using Functions.Shared.DTOs.Users;
using Functions.Shared.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace Functions.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(
        IGetUserProfilePicture getUserProfilePicture, 
        IGetUserByIdUseCase getUserById,
        IUpdateUserUseCase updateUserUseCase) : ControllerBase, IUserProxy
    {

        [HttpGet("getuserbyid")]
        public async Task<UserDTO> GetUserById([FromQuery] Guid id)
        {
            return await getUserById.Handle(id);
        }

        [HttpGet("getuserprofilepicture")]
        public async Task<ProfilePictureDTO> GetUserProfilePicture([FromQuery] Guid id)
        {
            return await getUserProfilePicture.Handle(id);
        }

        [HttpPost("updateuser")]
        public async Task<HttpResponseMessage> UpdaetUser([FromBody] UpdateUserRequestDTO updateUserRequestDTO)
        {
            try
            {
                await updateUserUseCase.Handle(updateUserRequestDTO);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}

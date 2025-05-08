using Functions.Server.Interfaces.Users;
using Functions.Server.Services;
using Functions.Shared.DTOs.Auth;
using Functions.Shared.DTOs.Users;
using Functions.Shared.Interfaces.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Functions.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(
        IGetUserByIdUseCase getUserById,
        IUpdateUserUseCase updateUserUseCase,
        IConfiguration configuration) : FunctionsControllerBase(configuration), IUserProxy
    {

        [HttpGet("getuser")]
        public async Task<UserDTO> GetUser()
        {
            var userId = await GetUserIdFromTokenAsync() ?? throw new UnauthorizedAccessException();

            return await getUserById.Handle(userId);
        }

        [HttpPut("updateuser")]
        public async Task<HttpResponseMessage> UpdateUser([FromBody] UpdateUserRequestDTO updateUserRequestDTO)
        {
            var userId = await GetUserIdFromTokenAsync() ?? throw new UnauthorizedAccessException();

            await updateUserUseCase.Handle(updateUserRequestDTO, userId);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

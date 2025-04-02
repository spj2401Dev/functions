using Functions.Server.Interfaces.Users;
using Functions.Shared.DTOs.Users;
using Functions.Shared.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace Functions.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(
        IGetAllUserUseCase getAllUser, 
        IGetUserByIdUseCase getUserById) : ControllerBase, IUserProxy
    {
        [HttpGet("getallusers")]
        public async Task<List<UserDTO>> GetAllUsers()
        {
            return await getAllUser.Handle();
        }

        [HttpGet("getuserbyid")]
        public async Task<UserDTO> GetUserById([FromQuery] Guid id)
        {
            return await getUserById.Handle(id);
        }
    }
}

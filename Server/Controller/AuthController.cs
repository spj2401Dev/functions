using Functions.Server.Interfaces.Auth;
using Functions.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Functions.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IRegistrationUseCase registration, ILoginUseCase login) : ControllerBase, IAuthProxy
    {
        [HttpPost("register")]
        public async Task<HttpResponseMessage> Register(RegisterRequestDTO request)
        {
            try
            {
                await registration.Handle(request);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO request)
        {
            try
            {
                var response = await login.Handle(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }
        }
    }
}

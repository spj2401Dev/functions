using Functions.Server.Interfaces.Auth;
using Functions.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Functions.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase, IAuthProxy
    {
        private readonly IRegistrationUseCase _registration;
        private readonly ILoginUseCase _login;

        public AuthController(IRegistrationUseCase registration, ILoginUseCase login)
        {
            _registration = registration;
            _login = login;
        }

        [HttpPost("register")]
        public async Task<HttpResponseMessage> Register(RegisterRequestDTO request)
        {
            try
            {
                await _registration.Handle(request);
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
                var response = await _login.Handle(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }
        }
    }
}

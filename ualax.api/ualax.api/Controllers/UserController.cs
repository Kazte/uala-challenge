using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using ualax.application.Features.Users.Login;
using ualax.application.Features.Users.Register;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace ualax.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var userHashed = await _mediator.Send(command);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("username", userHashed, cookieOptions);

            return Ok(new { Message = "Login Successful" });
        }
    }
}

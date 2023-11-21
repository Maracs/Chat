using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace PresentationLayer.Controllers
{
    [Route("api")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUsersService _userService;

        public IdentityController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<TokenDto>> SignInAsync([FromBody] LoginDto loginDto)
        {
            return Ok(await _userService.SignInAsync(loginDto));
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult<TokenDto>> SignUpAsync([FromBody] SignupDto signupDto)
        {
            return Ok(await _userService.SignUpAsync(signupDto));
        }
    }
}

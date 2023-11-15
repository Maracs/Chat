using BusinessLayer.DTOs;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Route("api")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UsersService _userService;

        public IdentityController(UsersService userService)
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

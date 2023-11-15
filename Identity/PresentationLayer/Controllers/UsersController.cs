using BusinessLayer.DTOs;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _userService;

        public UsersController(UsersService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserAsync([FromRoute] int userId)
        {
            return Ok();
        }

        [HttpGet("{id}/role")]
        [Authorize]
        public async Task<ActionResult<List<string>>> GetUserStatusesAsync([FromRoute] int userId)
        {
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<FullUserInfoDto>>> GetAllUsersAsync()
        {
            return Ok();
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<FullUserInfoDto>> GetProfileAsync()
        {
            
            return Ok();
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<ActionResult<FullUserInfoDto>> UpdateProfileAsync([FromBody] FullUserInfoWithoutIdDto userDto)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int userId)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<FullUserInfoDto>> UpdateUserAsync([FromRoute] int userId, [FromBody] FullUserInfoWithoutIdDto fullUserDto)
        {
            return Ok();
        }
    }
}

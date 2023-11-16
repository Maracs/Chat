using BusinessLayer.DTOs;
using BusinessLayer.Extentions;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;

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
        public async Task<ActionResult<UserDto>> GetUserAsync([FromRoute] int id)
        {
            return Ok(await _userService.GetUserAsync(id));
        }

        [HttpGet("{id}/statuses")]
        [Authorize]
        public async Task<ActionResult<List<Status>>> GetUserStatusesAsync([FromRoute] int id)
        {
            return Ok(await _userService.GetUserStatusesAsync(id));
        }

        [HttpPost("statuses")]
        [Authorize]
        public async Task<ActionResult> AddUserStatusAsync([FromBody] int sid)
        {
            var id = User.GetUserId();
            await _userService.AddUserStatusAsync(id, sid);
            return Ok();
        }

        [HttpDelete("statuses")]
        [Authorize]
        public async Task<ActionResult> DeleteUserStatusAsync([FromBody] int sid)
        {
            var id = User.GetUserId();
            await _userService.DeleteUserStatusAsync(id, sid);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<List<FullUserInfoDto>>> GetAllUsersAsync()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<FullUserInfoDto>> GetProfileAsync()
        {
            var id = User.GetUserId();
            return Ok(await _userService.GetProfileAsync(id));
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<ActionResult> UpdateProfileAsync([FromBody] FullUserInfoWithoutIdDto userDto)
        {
            var id = User.GetUserId();
            await _userService.UpdateProfileAsync(id, userDto);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<FullUserInfoDto>> UpdateUserAsync([FromRoute] int id, [FromBody] FullUserInfoWithoutIdDto userDto)
        {
            await _userService.UpdateProfileAsync(id, userDto);

            return Ok();
        }

        [HttpGet("{id}/photos")]
        [Authorize]
        public async Task<ActionResult<List<Image>>> GetPhotosAsync([FromRoute] int id)
        {
           
            return Ok(await _userService.GetPhotosAsync(id));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int id)
        {
            if (User.GetUserId() == id || User.IsInRole("admin"))
                await _userService.DeleteUserAsync(id);
            return Ok();
        }

        [HttpPost("photos")]
        [Authorize]
        public async Task<ActionResult> AddPhotosAsync([FromBody] List<string> photosSrc)
        {
            var userId = User.GetUserId();
            await _userService.AddPhotosAsync(userId,photosSrc);
            return Ok();
        }

        [HttpDelete("photos")]
        [Authorize]
        public async Task<ActionResult> DeletePhotosAsync([FromBody] List<int> photos)
        {
            var userId = User.GetUserId();
            await _userService.DeletePhotosAsync(userId, photos);
            return Ok();
        }

    }
}

using BusinessLayer.DTOs;
using BusinessLayer.Extentions;
using BusinessLayer.Interfaces;
using DataAccessLayer.Constants;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PresentationLayer.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
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
        public async Task<ActionResult> AddUserStatusAsync([FromBody] int statusId)
        {
            var id = User.GetUserId();
            await _userService.AddUserStatusAsync(id, statusId);

            return Ok();
        }

        [HttpDelete("statuses")]
        [Authorize]
        public async Task<ActionResult> DeleteUserStatusAsync([FromBody] int statusId)
        {
            var id = User.GetUserId();
            await _userService.DeleteUserStatusAsync(id, statusId);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<List<FullUserInfoDto>>> GetAllUsersAsync([FromQuery] int offset = 0,[FromQuery] int limit = 100)
        {
            return Ok(await _userService.GetAllUsersAsync(offset,limit));
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
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<FullUserInfoDto>> UpdateUserAsync([FromRoute] int id, [FromBody] FullUserInfoWithoutIdDto userDto)
        {
            await _userService.UpdateProfileAsync(id, userDto);

            return Ok();
        }

        [HttpGet("{id}/photos")]
        [Authorize]
        public async Task<ActionResult<List<ImageDto>>> GetPhotosAsync([FromRoute] int id)
        {
           
            return Ok(await _userService.GetPhotosAsync(id));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int id)
        {
            if (User.GetUserId() == id || User.IsInRole(RoleConstants.Admin))
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

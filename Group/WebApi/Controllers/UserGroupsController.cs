using Application.Dtos;
using Application.Extentions;
using Application.Ports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/groups/users")]
    [ApiController]
    [Authorize]
    public class UserGroupsController : ControllerBase
    {
        private readonly IUserGroupService _userGroupService;

        public UserGroupsController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] UserGroupDto userGroupDto, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _userGroupService.AddAsync(userId, userGroupDto, token);

            return NoContent();
        }

        [HttpPost("requests")]
        public async Task<ActionResult> RequestAsync([FromBody] UserGroupDto userGroupDto, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _userGroupService.RequestAsync(userId, userGroupDto, token);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromBody] UserGroupDto userGroupDto, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _userGroupService.DeleteAsync(userId, userGroupDto, token);

            return NoContent();
        }
    }
}

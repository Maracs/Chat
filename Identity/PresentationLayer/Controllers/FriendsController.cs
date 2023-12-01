using BusinessLayer.DTOs;
using BusinessLayer.Extentions;
using BusinessLayer.Interfaces;
using DataAccessLayer.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PresentationLayer.Controllers
{

    [ApiController]
    [Route("api/friends")]
    public class FriendsController : ControllerBase
    {

        private readonly IFriendsService _friendsService;

        public FriendsController(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<FullUserInfoDto>>> GetFriendsAsync([FromQuery] int offset = 0, [FromQuery] int limit = 100)
        {
            var userId = User.GetUserId();

            return Ok(await _friendsService.GetFriendsAsync(userId,offset,limit));
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult> AddFriendAsync([FromRoute] int fid)
        {
            var userId = User.GetUserId();
            await _friendsService.AddFriendAsync(userId, fid);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteFriendAsync([FromRoute] int fid)
        {
            var userId = User.GetUserId();
            await _friendsService.DeleteFriendAsync(userId, fid);

            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<List<FullUserInfoDto>>> GetUserFriendsAsync([FromRoute] int id, [FromQuery] int offset = 0, [FromQuery] int limit = 100)
        {
            return Ok(await _friendsService.GetFriendsAsync(id, offset, limit));
        }

    }
}

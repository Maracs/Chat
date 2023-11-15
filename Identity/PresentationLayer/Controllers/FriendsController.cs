using BusinessLayer.DTOs;
using BusinessLayer.Extentions;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{

    [ApiController]
    [Route("api/friends")]
    public class FriendsController : ControllerBase
    {

        private readonly FriendsService _friendsService;

        public FriendsController(FriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<FullUserInfoDto>>> GetFriendsAsync()
        {
            var userId = User.GetUserId();
            return Ok(await _friendsService.GetFriendsAsync(userId));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddFriendAsync([FromBody] int fid)
        {
            var userId = User.GetUserId();
            await _friendsService.AddFriendAsync(userId, fid);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteFriendAsync([FromBody] int fid)
        {
            var userId = User.GetUserId();
            await _friendsService.DeleteFriendAsync(userId, fid);
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<List<FullUserInfoDto>>> GetUserFriendsAsync([FromRoute] int id)
        {
            return Ok(await _friendsService.GetFriendsAsync(id));
        }

    }
}

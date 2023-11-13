using BusinessLayer.DTOs;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddFriendAsync([FromBody] int fid)
        {
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteFriendAsync([FromBody] int fid)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<FullUserInfoDto>>> GetUserFriendsAsync([FromRoute] int userId)
        {
            return Ok();
        }

    }
}

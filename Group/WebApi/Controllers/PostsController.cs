using Application.Dtos;
using Application.Extentions;
using Application.Ports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/groups")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{groupId}/posts")]
        public async Task<ActionResult<List<PostDto>>> GetAllAsync(CancellationToken token, [FromRoute] int groupId, [FromQuery] int offset = 0, [FromQuery] int limit = 100)
        {
            var userId = User.GetUserId();

            return Ok(await _postService.GetAllAsync(userId, groupId, offset, limit, token));
        }

        [HttpPost("{groupId}/posts")]
        public async Task<ActionResult> SendAsync([FromBody] PostDto postDto, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _postService.SendAsync(userId, postDto, token);

            return NoContent();
        }

        [HttpDelete("{groupId}/posts/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int groupId, [FromRoute] int id, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _postService.DeleteAsync(userId, groupId, id, token);

            return NoContent();
        }

        [HttpPut("{groupId}/posts/{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int groupId, [FromRoute] int id, [FromBody] string content, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _postService.UpdateAsync(userId, groupId, id, content, token);

            return NoContent();
        }
    }
}

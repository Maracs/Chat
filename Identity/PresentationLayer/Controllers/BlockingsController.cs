using BusinessLayer.DTOs;
using BusinessLayer.Extentions;
using BusinessLayer.Interfaces;
using DataAccessLayer.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{

    [ApiController]
    [Route("api/blocking")]
    public class BlockingsController : ControllerBase
    {

        private readonly IBlockingsService _blockingsService;

        public BlockingsController(IBlockingsService blockingsService)
        {
            _blockingsService = blockingsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<BlockingDto>>> GetBlockingsAsync([FromQuery] int offset = 0, [FromQuery] int limit = 100)
        {
            var userId = User.GetUserId();

            return Ok(await _blockingsService.GetBlockingsAsync(userId, offset, limit));
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult> AddBlockingAsync([FromRoute] int bid)
        {
            var userId = User.GetUserId();
            await _blockingsService.AddBlockingAsync(userId, bid);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteBlockingAsync([FromRoute] int bid)
        {
            var userId = User.GetUserId();
            await _blockingsService.DeleteBlockingAsync(userId, bid);

            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult<List<BlockingDto>>> GetUserBlockingsAsync([FromRoute] int id, [FromQuery] int offset = 0, [FromQuery] int limit = 100)
        {
            return Ok(await _blockingsService.GetBlockingsAsync(id,offset,limit));
        }
    }
}

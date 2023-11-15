using BusinessLayer.DTOs;
using BusinessLayer.Extentions;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{

    [ApiController]
    [Route("api/blocking")]
    public class BlockingsController : ControllerBase
    {

        private readonly BlockingsService _blockingsService;

        public BlockingsController(BlockingsService blockingsService)
        {
            _blockingsService = blockingsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<BlockingDto>>> GetBlockingsAsync()
        {
            var userId = User.GetUserId();
            return Ok(await _blockingsService.GetBlockingsAsync(userId));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddBlockingAsync([FromBody] int bid)
        {
            var userId = User.GetUserId();
            await _blockingsService.AddBlockingAsync(userId, bid);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteBlockingAsync([FromBody] int bid)
        {
            var userId = User.GetUserId();
            await _blockingsService.DeleteBlockingAsync(userId, bid);
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<List<BlockingDto>>> GetUserBlockingsAsync([FromRoute] int id)
        {
            return Ok(await _blockingsService.GetBlockingsAsync(id));
        }
    }
}

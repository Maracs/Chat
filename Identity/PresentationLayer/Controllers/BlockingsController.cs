using BusinessLayer.DTOs;
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
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddBlockingAsync([FromBody] int bid)
        {
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteBlockingAsync([FromBody] int bid)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<List<BlockingDto>>> GetUserBlockingsAsync([FromRoute] int userId)
        {
            return Ok();
        }
    }
}

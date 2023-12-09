using Application.Dtos;
using Application.Extentions;
using Application.Ports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;
        
        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChatDto>> GetByIdAsync([FromRoute] int id,CancellationTokenSource _source)
        {
            var userId = User.GetUserId();

            return Ok(await _chatService.GetByIdAsync(userId,id,_source.Token));
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatDto>>> GetAllAsync([FromQuery] int offset = 0, [FromQuery] int limit = 100,CancellationTokenSource _source)
        {
            var userId = User.GetUserId();

            return Ok(await _chatService.GetAllAsync(userId, offset, limit,_source.Token));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateChatDto chatDto,CancellationTokenSource _source)
        {
            var userId = User.GetUserId();
            await _chatService.CreateAsync(userId,chatDto, _source.Token);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id,CancellationTokenSource _source)
        {
            var userId = User.GetUserId();
            await _chatService.DeleteAsync(userId,id, _source.Token);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int id,[FromBody] CreateChatDto chatDto,CancellationTokenSource _source)
        {
            var userId = User.GetUserId();
            await _chatService.UpdateAsync(userId, id, chatDto, _source.Token);

            return NoContent();
        }
    }
}

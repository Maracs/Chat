using Application.Dtos;
using Application.Ports.Services;
using Application.Services;
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

        const int userId=1;

        public ChatsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChatDto>> GetByIdAsync([FromRoute] int id)
        {
            return Ok(await _chatService.GetByIdAsync(userId,id));
        }

        [HttpGet]
        public async Task<ActionResult<List<ChatDto>>> GetAllAsync()
        {
            return Ok(await _chatService.GetAllAsync(userId));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateChatDto chatDto)
        {
            await _chatService.CreateAsync(userId,chatDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _chatService.DeleteAsync(userId,id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ChatDto>> UpdateAsync([FromRoute] int id,[FromBody] CreateChatDto chatDto)
        {
            return Ok(await _chatService.UpdateAsync(userId,id, chatDto));
        }
    }
}

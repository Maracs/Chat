using Application.Dtos;
using Application.Ports.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPut("{chatid}/{id}/status")]
        public async Task<ActionResult<MessageDto>> ChangeMessageStatusAsync([FromRoute] int chatid, [FromRoute] int id, [FromBody] string status)
        {
            return Ok(await _messageService.ChangeMessageStatusAsync(chatid,id,status));
        }

        [HttpGet("{chatid}")]
        public async Task<ActionResult<List<MessageDto>>> GetAllAsync([FromRoute] int chatid)
        {
            return Ok(await _messageService.GetByIdAsync(chatid));
        }

        [HttpPost("{chatid}")]
        public async Task<ActionResult> SendAsync([FromBody] CreateChatDto chatDto)
        {
            await _messageService.SendAsync(chatDto);
            return Ok();
        }

        [HttpPost("{chatid}/resending")]
        public async Task<ActionResult> ResendAsync([FromBody] CreateChatDto chatDto)
        {
            await _messageService.ResendAsync(chatDto);
            return Ok();
        }

        [HttpDelete("{chatid}/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int chatid, [FromRoute] int id)
        {
            await _messageService.DeleteAsync(chatid,id);
            return Ok();
        }

        [HttpPut("{chatid}/{id}")]
        public async Task<ActionResult<MessageDto>> UpdateAsync([FromRoute] int chatid, [FromRoute] int id, [FromBody] MessageDto messageDto)
        {
            return Ok(await _messageService.UpdateAsync(chatid,id, messageDto));
        }
    }
}

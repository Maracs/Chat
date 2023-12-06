﻿using Application.Dtos;
using Application.Extentions;
using Application.Ports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly CancellationTokenSource _source;

        public MessagesController(IMessageService messageService, CancellationTokenSource source)
        {
            _messageService = messageService;
            _source = source;
        }

        

        [HttpPut("{chatid}/{id}/status")]
        public async Task<ActionResult> ChangeMessageStatusAsync([FromRoute] int chatid, [FromRoute] int id, [FromBody] string status)
        {
            var userId = User.GetUserId();
            await _messageService.ChangeMessageStatusAsync(userId, chatid, id, status,_source.Token);

            return NoContent();
        }

        [HttpGet("{chatid}")]
        public async Task<ActionResult<List<MessageDto>>> GetAllAsync([FromRoute] int chatid,[FromQuery] int offset = 0, [FromQuery] int limit = 100)
        {
            var userId = User.GetUserId();

            return Ok(await _messageService.GetAllAsync(userId,chatid, offset, limit,_source.Token));
        }

        [HttpPost("{chatid}")]
        public async Task<ActionResult> SendAsync([FromBody] MessageDto messageDto)
        {
            var userId = User.GetUserId();
            await _messageService.SendAsync(userId,messageDto,_source.Token);

            return NoContent();
        }

      
        [HttpDelete("{chatid}/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int chatid, [FromRoute] int id)
        {
            var userId = User.GetUserId();
            await _messageService.DeleteAsync(userId,chatid,id,_source.Token);

            return NoContent();
        }

        [HttpPut("{chatid}/{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int chatid, [FromRoute] int id, [FromBody] string content)
        {
            var userId = User.GetUserId();
            await _messageService.UpdateAsync(userId,chatid, id, content,_source.Token);

            return NoContent();
        }
    }
}

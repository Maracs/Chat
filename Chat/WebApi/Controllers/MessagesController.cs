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

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPut("{chatid}/message/{id}/status")]
        public async Task<ActionResult> ChangeMessageStatusAsync([FromRoute] int chatid, [FromRoute] int id, [FromBody] string status, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _messageService.ChangeMessageStatusAsync(userId, chatid, id, status, token);

            return NoContent();
        }

        [HttpGet("{chatid}")]
        public async Task<ActionResult<List<MessageDto>>> GetAllAsync(CancellationToken token, [FromRoute] int chatid, [FromQuery] int offset = 0, [FromQuery] int limit = 100)
        {
            var userId = User.GetUserId();

            return Ok(await _messageService.GetAllAsync(userId, chatid, offset, limit, token));
        }

        [HttpPost("{chatid}")]
        public async Task<ActionResult> SendAsync([FromBody] MessageDto messageDto, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _messageService.SendAsync(userId, messageDto, token);

            return NoContent();
        }


        [HttpDelete("{chatid}/message/{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int chatid, [FromRoute] int id, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _messageService.DeleteAsync(userId, chatid, id, token);

            return NoContent();
        }

        [HttpPut("{chatid}/message/{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int chatid, [FromRoute] int id, [FromBody] string content, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _messageService.UpdateAsync(userId, chatid, id, content, token);

            return NoContent();
        }
    }
}

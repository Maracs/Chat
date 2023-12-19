using Application.Dtos;
using Application.Extentions;
using Application.Ports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebApi.Controllers
{
    [Route("api/groups")]
    [ApiController]
    [Authorize]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> GetByIdAsync([FromRoute] int id, CancellationToken token)
        {
            var userId = User.GetUserId();

            return Ok(await _groupService.GetByIdAsync(userId, id, token));
        }

        [HttpGet]
        public async Task<ActionResult<List<GroupDto>>> GetAllAsync(CancellationToken token, [FromQuery] int offset = 0, [FromQuery] int limit = 100)
        {
            var userId = User.GetUserId();

            return Ok(await _groupService.GetAllAsync(userId, offset, limit, token));
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] CreateGroupDto groupDto, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _groupService.CreateAsync(userId, groupDto, token);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _groupService.DeleteAsync(userId, id, token);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromRoute] int id, [FromBody] CreateGroupDto groupDto, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _groupService.UpdateAsync(userId, id, groupDto, token);

            return NoContent();
        }
    }
}

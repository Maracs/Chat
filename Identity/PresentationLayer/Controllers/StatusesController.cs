using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/statuses")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
 
        private readonly IStatusesService _statusesService;

        public StatusesController(IStatusesService statusesService)
        {
            _statusesService = statusesService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<StatusDto>>> GetStatusesAsync([FromQuery] int offset = 0,[FromQuery] int limit = 100)
        {
            return Ok(await _statusesService.GetStatusesAsync(offset,limit));
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult> AddStatusAsync([FromBody] string status)
        {
            await _statusesService.AddStatusAsync(status);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult> UpdateStatusAsync([FromBody] StatusDto status)
        {
            await _statusesService.UpdateStatusAsync(status);

            return Ok();
        }
    }
}

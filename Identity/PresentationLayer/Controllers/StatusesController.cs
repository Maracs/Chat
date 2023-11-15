using BusinessLayer.DTOs;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/statuses")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
 
        private readonly StatusesService _statusesService;

        public StatusesController(StatusesService statusesService)
        {
            _statusesService = statusesService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Status>>> GetStatusesAsync()
        {
            return Ok(await _statusesService.GetStatusesAsync());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddStatusAsync([FromBody] string status)
        {
            await _statusesService.AddStatusAsync(status);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> UpdateStatusAsync([FromBody] StatusDto status)
        {
            await _statusesService.UpdateStatusAsync(status);
            return Ok();
        }
    }
}

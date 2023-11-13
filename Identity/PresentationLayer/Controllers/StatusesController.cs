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
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddStatusAsync([FromBody] string status)
        {
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateRStatusAsync([FromBody] Status status)
        {
            return Ok();
        }
    }
}

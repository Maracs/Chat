using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RolesService _roleService;

        public RolesController(RolesService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Role>>> GetRolesAsync()
        {
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddRoleAsync([FromBody] string role)
        {
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateRoleAsync([FromBody] Role role)
        {
            return Ok();    
        }

    }
}

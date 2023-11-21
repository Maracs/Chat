using BusinessLayer.DTOs;
using BusinessLayer.Interfaces;
using DataAccessLayer.Constants;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _roleService;

        public RolesController(IRolesService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Role>>> GetRolesAsync([FromQuery] int offset = 0, [FromQuery] int limit = 100)
        {
            return Ok(await _roleService.GetRolesAsync(offset,limit));
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult> AddRoleAsync([FromBody] string role)
        {
            await _roleService.AddRoleAsync(role);

            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<ActionResult> UpdateRoleAsync([FromBody] RoleDto role)
        {
            await _roleService.UpdateRoleAsync(role);

            return Ok();    
        }

    }
}

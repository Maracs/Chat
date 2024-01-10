using BusinessLayer.Contracts;
using BusinessLayer.Dtos;
using BusinessLayer.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<SettingDto>>> GetSettingAsync(CancellationToken token)
        {
            var userId = User.GetUserId();

            return Ok(await _settingsService.GetSettingAsync(userId, token));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddSettingAsync([FromBody] SettingDto settingDto,CancellationToken token)
        {
            var userId = User.GetUserId();
            await _settingsService.AddSettingAsync(settingDto,userId, token);

            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateSettingAsync([FromBody] SettingDto settingDto, CancellationToken token)
        {
            var userId = User.GetUserId();
            await _settingsService.UpdateSettingAsync(settingDto,userId, token);

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteSettingAsync(CancellationToken token)
        {
            var userId = User.GetUserId();
            await _settingsService.DeleteSettingAsync(userId, token);

            return Ok();
        }
    }
}

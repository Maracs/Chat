using Application.Dtos;
using Application.Extentions;
using Application.Ports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserChatsController : ControllerBase
    {
        private readonly IUserChatService _userChatsService;
        private readonly CancellationTokenSource _source;


        public UserChatsController(IUserChatService userChatsService,CancellationTokenSource source)
        {
            _userChatsService = userChatsService;
            _source = source;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] UserChatDto userChatDto)
        {
            var userId = User.GetUserId();
            await _userChatsService.CreateAsync(userId,userChatDto,_source.Token);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromBody] UserChatDto userChatDto)
        {
            var userId = User.GetUserId();
            await _userChatsService.DeleteAsync(userId,userChatDto,_source.Token);

            return NoContent();
        }
    }
}

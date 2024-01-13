using BusinessLayer.Extentions;
using DataAccessLayer.Contracts;
using Microsoft.AspNetCore.Http;


namespace BusinessLayer.Middlewares
{
    public class UserCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserRequestRepository _userRequestRepository;

        public UserCacheMiddleware(RequestDelegate next, IUserRequestRepository userRequestRepository)
        {
            _next = next;
            _userRequestRepository = userRequestRepository;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var principal = context.User;
                
                if (principal == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }
                if (principal.Identity.IsAuthenticated)
                {
                    var userId = principal.GetUserId();
                    _userRequestRepository.SetLatestRequestTime(userId.ToString(), DateTime.Now);
                }
            }
            finally
            {
                await _next.Invoke(context);
            }
        }
    }
}

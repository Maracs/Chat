using Application.Extentions;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace WebApi.Middlewares
{
    public class UserCacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _userRequestsKey;
        private IDatabase _db;

        public UserCacheMiddleware(RequestDelegate next, IDatabase db)
        {
            _next = next;
            _db = db;
            _userRequestsKey = "latest_user_requests";
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

                var userId = principal.GetUserId();
                SetLatestRequestTime(userId.ToString(), DateTime.Now);
            }
            finally
            {
                await _next.Invoke(context);
            }
        }

        private void SetLatestRequestTime(string userId, DateTime timestamp)
        {
            var key = GetKey(userId);
            SetData(key, timestamp.ToString(), TimeSpan.FromSeconds(10));
        }

        private bool SetData<T>(string key, T value, TimeSpan expirationTime)
        {
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expirationTime);

            return isSet;
        }

        private string GetKey(string userId)
        {
            return $"{_userRequestsKey}:{userId}";
        }
    }
}

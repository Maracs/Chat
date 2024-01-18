using DataAccessLayer.Contracts;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace DataAccessLayer.Repositories
{
    public class UserRequestRepository :IUserRequestRepository
    {
        private readonly string _userRequestsKey = "latest_user_requests";
        private IDatabase _db;

        public UserRequestRepository(string connectionString)
        {    
            var configurationOptions = ConfigurationOptions.Parse(connectionString);
            _db = ConnectionMultiplexer.Connect(configurationOptions).GetDatabase();
        }

        public DateTime? GetLatestRequestTime(string userId)
        {
            var key = GetKey(userId);
            var timestampString = GetData(key);

            if (!string.IsNullOrEmpty(timestampString) && DateTime.TryParse(timestampString, out DateTime timestamp))
            {
                    return timestamp;
            }

            return null;
        }

        public void SetLatestRequestTime(string userId, DateTime timestamp)
        {
            var key = GetKey(userId);
            SetData(key, timestamp.ToString(), TimeSpan.FromSeconds(10));
        }

        public void DeleteLatestRequestTime(string userId)
        {
            var key = GetKey(userId);
            RemoveData(key);
        }

        private string GetData(string key)
        {
            var value = _db.StringGet(key);

            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<string>(value);
            }

            return default;
        }

        private bool SetData(string key, string value, TimeSpan expirationTime)
        {
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expirationTime);

            return isSet;
        }

        private bool RemoveData(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);

            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }

            return false;
        }

        private string GetKey(string userId)
        {
            return $"{_userRequestsKey}:{userId}";
        }
    }
}

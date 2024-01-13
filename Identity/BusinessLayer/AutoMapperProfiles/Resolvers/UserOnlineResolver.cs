using AutoMapper;
using DataAccessLayer.Contracts;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;


namespace BusinessLayer.AutoMapperProfiles.Resolvers
{
    public class UserOnlineResolver : IValueResolver<User, object, bool>
    {
        private readonly IUserRequestRepository _userRequestRepository;

        public UserOnlineResolver(IUserRequestRepository userRequestRepository)
        {
            _userRequestRepository = userRequestRepository;
        }

        public bool Resolve(
            User source,
            object destination,
            bool destMember,
            ResolutionContext context
        )
        {
            var latestRequestTime = _userRequestRepository.GetLatestRequestTime(source.Id.ToString());

            return latestRequestTime != null;
        }
    }
}

using Shared;
using Domain.Interfaces;
using MassTransit;


namespace Application.Consumers
{
    public class UserDeletedConsumer : IConsumer<UserIdForGroupDto>
    {
        private readonly IUserGroupRepository _userGroupRepository;
        

        public UserDeletedConsumer(IUserGroupRepository userGroupRepository )
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task Consume(ConsumeContext<UserIdForGroupDto> context)
        {
            _userGroupRepository.DeleteUserFromGroups(context.Message.UserId);
            await _userGroupRepository.SaveChangesAsync(context.CancellationToken);
        }
    }
}

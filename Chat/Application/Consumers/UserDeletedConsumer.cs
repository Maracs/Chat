using Shared;
using Domain.Interfaces;
using MassTransit;


namespace Application.Consumers
{
    public class UserDeletedConsumer : IConsumer<UserIdForChatDto>
    {
        private readonly IUserChatRepository _userChatRepository;


        public UserDeletedConsumer(IUserChatRepository userChatRepository)
        {
            _userChatRepository = userChatRepository;
        }

        public async Task Consume(ConsumeContext<UserIdForChatDto> context)
        {
            _userChatRepository.DeleteUserFromChats(context.Message.UserId);
            await _userChatRepository.SaveChangesAsync();
        }
    }
}


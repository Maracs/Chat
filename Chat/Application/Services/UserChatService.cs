using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class UserChatService : IUserChatService
    {
        private readonly IUserChatRepository _userChatRepository;
        private readonly IChatRepository _chatRepository;
        private readonly ILogger<UserChatService> _logger;

        public UserChatService(IUserChatRepository userChatRepository, IChatRepository chatRepository, ILogger<UserChatService> logger)
        {
            _userChatRepository = userChatRepository;
            _chatRepository = chatRepository;
            _logger = logger;
        }

        public async Task CreateAsync(int userId, UserChatDto userChatDto, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Add User with id {Id} in Chat with id {Id}.", userId, userChatDto.UserId, userChatDto.ChatId);


            var creatorId = (await _chatRepository.GetByIdAsync(userChatDto.ChatId)).CreatorId;

            if (userId != creatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            await _userChatRepository.CreateAsync(new ChatUser() { ChatId = userChatDto.ChatId, UserId = userChatDto.UserId, JoinTime = DateTime.Now });
            token.ThrowIfCancellationRequested();
            await _userChatRepository.SaveChangesAsync();

            _logger.LogInformation("User with id {UserId} Add User with id {Id} in Chat with id {Id} successfully.", userId, userChatDto.UserId, userChatDto.ChatId);
        }

        public async Task DeleteAsync(int userId, UserChatDto userChatDto, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Delete User with id {Id} from Chat with id {Id}.", userId, userChatDto.UserId, userChatDto.ChatId);

            var creatorId = (await _chatRepository.GetByIdAsync(userChatDto.ChatId)).CreatorId;

            if (userId != creatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _userChatRepository.Delete(new ChatUser() { ChatId = userChatDto.ChatId, UserId = userChatDto.UserId });
            token.ThrowIfCancellationRequested();
            await _userChatRepository.SaveChangesAsync();

            _logger.LogInformation("User with id {UserId} Delete User with id {Id} from Chat with id {Id} successfully.", userId, userChatDto.UserId, userChatDto.ChatId);
        }
    }
}

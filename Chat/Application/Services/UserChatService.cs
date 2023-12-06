using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserChatService : IUserChatService
    {
        private readonly IUserChatRepository _userChatRepository;
        private readonly IChatRepository _chatRepository;

        public UserChatService(IUserChatRepository userChatRepository, IChatRepository chatRepository)
        {
            _userChatRepository = userChatRepository;
            _chatRepository = chatRepository;
        }

        public async Task CreateAsync(int userId, UserChatDto userChatDto, CancellationToken token)
        {
            var creatorId = (await _chatRepository.GetByIdAsync(userChatDto.ChatId)).CreatorId;

            if (userId != creatorId)
            {
               throw new ApiException("Invalid operation", ExceptionStatus.Status.BadRequest);
            }

            await _userChatRepository.CreateAsync(new ChatUser() {ChatId = userChatDto.ChatId,UserId = userChatDto.UserId, JoinTime = DateTime.Now });
            token.ThrowIfCancellationRequested();
            await _userChatRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId, UserChatDto userChatDto, CancellationToken token)
        {
            var creatorId = (await _chatRepository.GetByIdAsync(userChatDto.ChatId)).CreatorId;

            if (userId != creatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.Status.BadRequest);
            }

            _userChatRepository.Delete(new ChatUser() { ChatId = userChatDto.ChatId, UserId = userChatDto.UserId });
            token.ThrowIfCancellationRequested();
            await _userChatRepository.SaveChangesAsync();
        }
    }   
}

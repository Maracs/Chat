using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MessageService> _logger;

        public MessageService(IMessageRepository messageRepository, IChatRepository chatRepository, IMapper mapper, ILogger<MessageService> logger)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task ChangeMessageStatusAsync(int userId, int chatid, int id, string status, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Change status of Message with id {Id}.", userId, id);

            var chat = await _chatRepository.GetByIdAsync(chatid);
            var user = chat.Users
                           .Where(user => user.UserId == userId)
                           .FirstOrDefault();

            if (null == user && chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            await _messageRepository.ChangeMessageStatusAsync(chatid, id, status);
            token.ThrowIfCancellationRequested();
            await _messageRepository.SaveChangesAsync();

            _logger.LogInformation("User with id { UserId} successfully Change status of  Message with id { Id}.", userId, id);
        }

        public async Task DeleteAsync(int userId, int chatid, int id, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Delete Message with id {Id}.", userId, id);

            var chat = await _chatRepository.GetByIdAsync(chatid);
            var user = chat.Users
                           .Where(user => user.UserId == userId)
                           .FirstOrDefault();

            if (null == user && chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _messageRepository.Delete(chatid, id);
            token.ThrowIfCancellationRequested();
            await _messageRepository.SaveChangesAsync();

            _logger.LogInformation("User with id {UserId} successfully Delete Message with id {Id}.", userId, id);
        }

        public async Task<List<MessageDto>> GetAllAsync(int userId, int chatid, int offset, int limit, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Get All Messages.", userId);

            var messages = await _messageRepository.GetAllAsync(userId, chatid, offset, limit);
            token.ThrowIfCancellationRequested();

            _logger.LogInformation("User with id {UserId} Get all Messages successfully.", userId);

            return _mapper.Map<List<MessageDto>>(messages);
        }

        public async Task SendAsync(int userId, MessageDto messageDto, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Create Message.", userId);

            if (userId != messageDto.UserId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var message = _mapper.Map<Message>(messageDto);
            var chatMessage = _mapper.Map<ChatMessage>(messageDto);
            chatMessage.MessageStatusId = await _messageRepository.GetStatusAsync(messageDto.Status);
            await _messageRepository.SendAsync(chatMessage, message);
            token.ThrowIfCancellationRequested();
            await _messageRepository.SaveChangesAsync();

            _logger.LogInformation("User with id {UserId} successfully Create Message.", userId);
        }

        public async Task UpdateAsync(int userId, int chatid, int id, string content, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Update Message with id {Id}.", userId, id);

            var chat = await _chatRepository.GetByIdAsync(chatid);
            var user = chat.Users
                           .Where(user => user.UserId == userId)
                           .FirstOrDefault();

            if (null == user && chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var chatMessage = await _messageRepository.GetByIdAsync(chatid, id);
            var message = chatMessage.Message;
            message.Content = content;
            _messageRepository.Update(message);
            token.ThrowIfCancellationRequested();
            await _messageRepository.SaveChangesAsync();

            _logger.LogInformation("User with id {UserId} Update Message with id {Id} successfully.", userId, id);
        }
    }
}

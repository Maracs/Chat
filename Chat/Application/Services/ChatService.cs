using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ChatService> _logger;

        public ChatService(IChatRepository chatRepository, IMapper mapper, ILogger<ChatService> logger)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAsync(int userId, CreateChatDto chatDto, CancellationToken token)
        {
            _logger.LogInformation("Trying to call CreateAsync.");

            if (userId != chatDto.CreatorId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            var user = new Chat()
            {
                Name = chatDto.Name,
                CreatorId = chatDto.CreatorId,
                Info = chatDto.Info,
            };
            await _chatRepository.CreateAsync(user);
            token.ThrowIfCancellationRequested();
            await _chatRepository.SaveChangesAsync();

            _logger.LogInformation("CreateAsync was called successfully.");
        }

        public async Task DeleteAsync(int userId, int id, CancellationToken token)
        {
            _logger.LogInformation("Trying to call DeleteAsync.");

            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _chatRepository.Delete(id);
            token.ThrowIfCancellationRequested();
            await _chatRepository.SaveChangesAsync();

            _logger.LogInformation("DeleteAsync was called successfully.");
        }

        public async Task<List<ChatDto>> GetAllAsync(int userId, int offset, int limit, CancellationToken token)
        {
            _logger.LogInformation("Trying to call GetAllAsync.");

            var chats = _mapper.Map<List<ChatDto>>(await _chatRepository.GetAllAsync(userId, offset, limit));

            _logger.LogInformation("GetAllAsync was called successfully.");

            return chats;
        }

        public async Task<ChatDto> GetByIdAsync(int userId, int id, CancellationToken token)
        {
            _logger.LogInformation("Trying to call GetByIdAsync.");

            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            token.ThrowIfCancellationRequested();

            _logger.LogInformation("GetByIdAsync was called successfully.");

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task UpdateAsync(int userId, int id, CreateChatDto chatDto, CancellationToken token)
        {
            _logger.LogInformation("Trying to call UpdateAsync.");

            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            _mapper.Map<CreateChatDto, Chat>(chatDto, chat);
            _chatRepository.Update(chat);
            token.ThrowIfCancellationRequested();
            await _chatRepository.SaveChangesAsync();

            _logger.LogInformation("UpdateAsync was called successfully.");
        }
    }
}

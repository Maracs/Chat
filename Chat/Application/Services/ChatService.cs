using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Grpc.Dtos;
using Grpc.Interfaces;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly IUserNicknameService _userNicknameService;
        private readonly ILogger<ChatService> _logger;

        public ChatService(IChatRepository chatRepository, IMapper mapper, ILogger<ChatService> logger, IUserNicknameService userNicknameService))
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _logger = logger;
             _userNicknameService = userNicknameService;

        }

        public async Task CreateAsync(int userId, CreateChatDto chatDto, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Create Chat.",userId);

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

            _logger.LogInformation("User with id {UserId} successfully Create Chat.",userId);
        }

        public async Task DeleteAsync(int userId, int id, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Delete Chat with id {Id}.", userId,id);

            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _chatRepository.Delete(id);
            token.ThrowIfCancellationRequested();
            await _chatRepository.SaveChangesAsync();

            _logger.LogInformation("User with id {UserId} successfully Delete Chat with id {Id}.", userId,id);
        }

        public async Task<List<ChatDto>> GetAllAsync(int userId, int offset, int limit, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Get All Chats.",userId);

            var chats = _mapper.Map<List<ChatDto>>(await _chatRepository.GetAllAsync(userId, offset, limit));

            _logger.LogInformation("User with id {UserId} Get all Chats successfully.",userId);

            return chats;
        }

        public async Task<ChatWithUserNicknameDto> GetByIdAsync(int userId, int id, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Get Chat with id {Id}.",userId,id);

            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            token.ThrowIfCancellationRequested();

            var userNickname = await _userNicknameService.GetUserNicknameAsync(new UserIdDto() { Id = userId });
            var chatDto = _mapper.Map<ChatWithUserNicknameDto>(chat);
            chatDto = _mapper.Map(userNickname, chatDto);
            _logger.LogInformation("User with id {UserId} Get Chat with id {Id} successfully.", userId, id);

            return chatDto;
        }

        public async Task UpdateAsync(int userId, int id, CreateChatDto chatDto, CancellationToken token)
        {
            _logger.LogInformation("User with id {UserId} trying to Update Chat with id {Id}.", userId, id);

            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            _mapper.Map<CreateChatDto, Chat>(chatDto, chat);
            _chatRepository.Update(chat);
            token.ThrowIfCancellationRequested();
            await _chatRepository.SaveChangesAsync();

            _logger.LogInformation("User with id {UserId} Update Chat with id {Id} successfully.", userId, id);
        }
    }
}

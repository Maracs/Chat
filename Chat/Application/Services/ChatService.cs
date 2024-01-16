using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Grpc.Dtos;
using Grpc.Interfaces;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly IUserNicknameService _userNicknameService;
        public ChatService(IChatRepository chatRepository, IMapper mapper, IUserNicknameService userNicknameService)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _userNicknameService = userNicknameService;
        }

        public async Task CreateAsync(int userId, CreateChatDto chatDto, CancellationToken token)
        {
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
        }

        public async Task DeleteAsync(int userId, int id, CancellationToken token)
        {
            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            }

            _chatRepository.Delete(id);
            token.ThrowIfCancellationRequested();
            await _chatRepository.SaveChangesAsync();
        }

        public async Task<List<ChatDto>> GetAllAsync(int userId, int offset, int limit, CancellationToken token)
        {
            return _mapper.Map<List<ChatDto>>(await _chatRepository.GetAllAsync(userId, offset, limit));
        }

        public async Task<ChatWithUserNicknameDto> GetByIdAsync(int userId, int id, CancellationToken token)
        {
            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            token.ThrowIfCancellationRequested();

            var userNickname = await _userNicknameService.GetUserNicknameAsync(new UserIdDto() { Id = userId });
            var chatDto = _mapper.Map<ChatWithUserNicknameDto>(chat);
            chatDto = _mapper.Map(userNickname, chatDto);

            return chatDto;
        }

        public async Task UpdateAsync(int userId, int id, CreateChatDto chatDto, CancellationToken token)
        {
            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ExceptionStatus.BadRequest);
            };

            _mapper.Map<CreateChatDto, Chat>(chatDto, chat);
            _chatRepository.Update(chat);
            token.ThrowIfCancellationRequested();
            await _chatRepository.SaveChangesAsync();
        }
    }
}

using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services
{
    public class ChatService : IChatService
    {

        private readonly IChatRepository _chatRepository;

        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }


        public async Task CreateAsync(int userId, CreateChatDto chatDto)
        {
            if(userId != chatDto.CreatorId)
            {
                throw new ApiException("Invalid operation", ApiException.ExceptionStatus.BadRequest);
            }

            var user = new Chat()
            {
                Name = chatDto.Name,
                CreatorId = chatDto.CreatorId,
                Info = chatDto.Info,
            };

           await _chatRepository.CreateAsync(user);

           await _chatRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId, int id)
        {
            var chat = await _chatRepository.GetByIdAsync(id);

            if(chat.CreatorId!= userId)
            {
                throw new ApiException("Invalid operation", ApiException.ExceptionStatus.BadRequest);
            }

            _chatRepository.Delete(id);

            await _chatRepository.SaveChangesAsync();
        }

        public async Task<List<ChatDto>> GetAllAsync(int userId, int offset, int limit)
        {
            return _mapper.Map<List<ChatDto>>(await _chatRepository.GetAllAsync(userId,offset,limit));
        }

        public async Task<ChatDto> GetByIdAsync(int userId, int id)
        {
            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ApiException.ExceptionStatus.BadRequest);
            };

            return _mapper.Map<ChatDto>(chat);
        }

        public async Task UpdateAsync(int userId, int id, CreateChatDto chatDto)
        {
            var chat = await _chatRepository.GetByIdAsync(id);

            if (chat.CreatorId != userId)
            {
                throw new ApiException("Invalid operation", ApiException.ExceptionStatus.BadRequest);
            }; 

            chat.CreatorId = chatDto.CreatorId;

            chat.Info = chatDto.Info;

            chat.Name = chatDto.Name;

            _chatRepository.Update(chat);
            await _chatRepository.SaveChangesAsync();
        }
    }
}

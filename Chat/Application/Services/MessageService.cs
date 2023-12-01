using Application.Dtos;
using Application.Exceptions;
using Application.Ports.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;


namespace Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        private readonly IChatRepository _chatRepository;

        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IChatRepository chatRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
            _mapper = mapper;
        }


        public async Task ChangeMessageStatusAsync(int userId, int chatid, int id, string status)
        {
            if(userId!=id)
            {
                throw new ApiException("Invalid operation", ApiException.ExceptionStatus.BadRequest);
            }
            await _messageRepository.ChangeMessageStatusAsync(chatid,id,status);
            await _messageRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId, int chatid, int id)
        {
            if (null == (await _chatRepository.GetByIdAsync(chatid))
                .Users
                .Where(user=>user.UserId == userId)
                .FirstOrDefault())
            {
                throw new ApiException("Invalid operation", ApiException.ExceptionStatus.BadRequest);
            }
            _messageRepository.Delete(chatid,id);

            await  _messageRepository.SaveChangesAsync();
        }

        public async Task<List<MessageDto>> GetAllAsync(int userId, int chatid, int offset, int limit)
        {
            return _mapper.Map<List<MessageDto>>(await _messageRepository.GetAllAsync( userId,chatid, offset,limit));
        }

        public async Task SendAsync(int userId, MessageDto messageDto)
        {
            if(userId!=messageDto.UserId)
            {
                throw new ApiException("Invalid operation", ApiException.ExceptionStatus.BadRequest);
            }

            var message = _mapper.Map<Message>(messageDto);

            var chatMessage = _mapper.Map<ChatMessage>(messageDto);

            await _messageRepository.SendAsync(chatMessage, message);

            await _messageRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int userId, int chatid, int id, string content)
        {
            if (null == (await _chatRepository.GetByIdAsync(chatid))
                .Users
                .Where(user => user.UserId == userId)
                .FirstOrDefault())
            {
                throw new ApiException("Invalid operation", ApiException.ExceptionStatus.BadRequest);
            }

            var chatMessage = await _messageRepository.GetByIdAsync(chatid,id);

            var message = chatMessage.Message;

            message.Content = content;

            _messageRepository.Update(message);

            await _messageRepository.SaveChangesAsync();


        }
    }
}

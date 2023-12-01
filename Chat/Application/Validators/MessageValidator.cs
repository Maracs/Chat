using Application.Dtos;
using FluentValidation;


namespace Application.Validators
{
    public class MessageValidator:AbstractValidator<MessageDto>
    {
        public MessageValidator()
        {
            RuleFor(dto=>dto.Content).NotEmpty();

            RuleFor(dto=>dto.SendTime).NotEmpty();

            RuleFor(dto=>dto.ChatId).NotEmpty();

            RuleFor(dto => dto.UserId).NotEmpty();

            RuleFor(dto=>dto.Status).NotEmpty();
        }
    }
}

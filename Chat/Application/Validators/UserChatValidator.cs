using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class UserChatValidator:AbstractValidator<UserChatDto>
    {
        public UserChatValidator()
        {
            RuleFor(dto=>dto.UserId).NotEmpty();
            RuleFor(dto=>dto.ChatId).NotEmpty();
        }
    }
}

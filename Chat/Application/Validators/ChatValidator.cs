using Application.Dtos;
using FluentValidation;


namespace Application.Validators
{
    public class ChatValidator:AbstractValidator<ChatDto>
    {
        public ChatValidator()
        {
            RuleFor(dto =>dto.CreatorId).NotEmpty();

            RuleFor(dto=>dto.Name).NotEmpty();

            RuleFor(dto=>dto.Id).NotEmpty();
        }
    }
}

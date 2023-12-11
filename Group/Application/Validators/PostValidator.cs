using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class MessageValidator : AbstractValidator<PostDto>
    {
        public MessageValidator()
        {
            RuleFor(dto => dto.Content).NotEmpty();
            RuleFor(dto => dto.SendTime).NotEmpty();
            RuleFor(dto => dto.GroupId).NotEmpty();
        }
    }
}

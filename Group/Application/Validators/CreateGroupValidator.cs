using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class CreateChatValidator : AbstractValidator<CreateGroupDto>
    {
        public CreateChatValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty();
            RuleFor(dto => dto.CreatorId).NotEmpty();
        }
    }
}

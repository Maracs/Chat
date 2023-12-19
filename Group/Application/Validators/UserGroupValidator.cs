using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class UserChatValidator : AbstractValidator<UserGroupDto>
    {
        public UserChatValidator()
        {
            RuleFor(dto => dto.UserId).NotEmpty();

            RuleFor(dto => dto.GroupId).NotEmpty();
        }
    }
}

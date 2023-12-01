using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

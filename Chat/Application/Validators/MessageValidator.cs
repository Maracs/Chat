using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

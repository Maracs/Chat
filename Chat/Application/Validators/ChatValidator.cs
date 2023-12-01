using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

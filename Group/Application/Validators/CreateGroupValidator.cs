﻿using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class CreateChatValidator : AbstractValidator<CreateGroupDto>
    {
        public CreateChatValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().Matches("^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$");

            RuleFor(dto => dto.CreatorId).NotEmpty();
        }
    }
}

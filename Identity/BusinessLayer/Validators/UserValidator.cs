using BusinessLayer.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators
{
    public class UserValidator:AbstractValidator<FullUserInfoWithoutIdDto>
    {
        public UserValidator()
        {
            RuleFor(dto => dto.NickName).NotEmpty();

            RuleFor(dto => dto.AccountName).NotEmpty();  

            RuleFor(dto => dto.Phone).NotEmpty().Matches(@"([+]?[0-9\s-\(\)]{3,25})*$");
        }
    }
}

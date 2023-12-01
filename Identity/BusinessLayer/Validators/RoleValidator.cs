using BusinessLayer.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators
{
    public class RoleValidator:AbstractValidator<RoleDto>
    {
        public RoleValidator()
        {
            RuleFor(dto => dto.Id).NotEmpty();

            RuleFor(dto => dto.Name).NotEmpty();
        }
    }
}

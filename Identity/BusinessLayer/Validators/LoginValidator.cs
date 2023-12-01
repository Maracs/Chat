using BusinessLayer.DTOs;
using FluentValidation;


namespace BusinessLayer.Validators
{
    public class LoginValidator:AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(dto => dto.Passhash).NotEmpty();

            RuleFor(dto => dto.AccountName).NotEmpty();
        }
    }
}

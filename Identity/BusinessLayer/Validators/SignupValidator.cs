using BusinessLayer.DTOs;
using FluentValidation;


namespace BusinessLayer.Validators
{
    public class SignupValidator:AbstractValidator<SignupDto>
    {
        public SignupValidator()
        {
            RuleFor(dto => dto.Passhash).NotEmpty();

            RuleFor(dto => dto.AccountName).NotEmpty();

            RuleFor(dto => dto.Nickname).NotEmpty();

            RuleFor(dto => dto.PhoneNumber).NotEmpty().Matches(@"([+]?[0-9\s-\(\)]{3,25})*$");
        }
    }
}

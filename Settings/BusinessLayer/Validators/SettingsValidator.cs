using BusinessLayer.Dtos;
using FluentValidation;


namespace BusinessLayer.Validators
{
    public class SettingsValidator : AbstractValidator<SettingDto>
    {
        public SettingsValidator()
        {
            RuleFor(dto => dto.ThemeColor).NotEmpty();
        }
    }
}

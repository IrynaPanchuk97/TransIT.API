using FluentValidation;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.DAL.Models.Validations
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(p => p.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30);
        }
    }
}

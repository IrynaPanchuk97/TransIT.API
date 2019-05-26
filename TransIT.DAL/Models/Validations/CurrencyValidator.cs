using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class CurrencyValidator : AbstractValidator<CurrencyDTO>
    {
        public CurrencyValidator()
        {
            RuleFor(t => t.FullName)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.ShortName)
                .NotNull()
                .NotEmpty();
        }
    }
}

using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class CountryValidator : AbstractValidator<CountryDTO>
    {
        public CountryValidator()
        {
            RuleFor(t => t.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}

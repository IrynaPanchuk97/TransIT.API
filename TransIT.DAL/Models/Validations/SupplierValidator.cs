using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class SupplierValidator : AbstractValidator<SupplierDTO>
    {
        public SupplierValidator()
        {
            RuleFor(t => t.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.Country)
                .NotNull();
            RuleFor(x => x.Currency)
                .NotNull();
            RuleFor(t => t.Country.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.Currency.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x.FullName)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.Edrpou)
                .NotNull()
                .NotEmpty();
        }
    }
}

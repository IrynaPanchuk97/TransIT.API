using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class IssueValidator : AbstractValidator<IssueDTO>
    {
        public IssueValidator()
        {
            RuleFor(t => t.Summary)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Vehicle)
                .NotNull();
            RuleFor(x => x.Vehicle.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}

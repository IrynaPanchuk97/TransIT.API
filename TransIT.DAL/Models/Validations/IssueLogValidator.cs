using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class IssueLogValidator : AbstractValidator<IssueLogDTO>
    {
        public IssueLogValidator()
        {
            RuleFor(t => t.Issue)
                .NotNull();
            RuleFor(t => t.Issue.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(t => t.ActionType)
                .NotNull();
            RuleFor(t => t.ActionType.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}

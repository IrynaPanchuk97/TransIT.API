using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class TransitionValidator : AbstractValidator<TransitionDTO>
    {
        public TransitionValidator()
        {
            RuleFor(t => t.ActionType)
                .NotNull();
            RuleFor(t => t.FromState)
                .NotNull();
            RuleFor(t => t.ToState)
                .NotNull();
            RuleFor(t => t.ActionType.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(t => t.FromState.Id)
                .NotNull()
                .GreaterThan(0);
            RuleFor(t => t.ToState.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}

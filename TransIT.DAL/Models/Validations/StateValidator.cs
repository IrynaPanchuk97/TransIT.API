using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class StateValidator : AbstractValidator<StateDTO>
    {
        public StateValidator()
        {
            RuleFor(t => t.TransName)
                .NotNull()
                .NotEmpty();
        }
    }
}

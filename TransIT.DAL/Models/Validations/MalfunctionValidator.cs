using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class MalfunctionValidator : AbstractValidator<MalfunctionDTO>
    {
        public MalfunctionValidator()
        {
            RuleFor(t => t.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.MalfunctionSubgroup)
                .NotNull();
            RuleFor(t => t.MalfunctionSubgroup.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}

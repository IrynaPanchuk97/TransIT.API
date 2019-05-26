using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class MalfunctionGroupValidator : AbstractValidator<MalfunctionGroupDTO>
    {
        public MalfunctionGroupValidator()
        {
            RuleFor(t => t.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}

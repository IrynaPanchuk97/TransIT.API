using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class RoleValidator : AbstractValidator<RoleDTO>
    {
        public RoleValidator()
        {
            RuleFor(t => t.TransName)
                .NotNull()
                .NotEmpty();
        }
    }
}

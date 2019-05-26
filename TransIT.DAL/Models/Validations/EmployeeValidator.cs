using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class EmployeeValidator : AbstractValidator<EmployeeDTO>
    {
        public EmployeeValidator()
        {
            RuleFor(t => t.BoardNumber)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.ShortName)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.FirstName)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.LastName)
                .NotNull()
                .NotEmpty();
        }
    }
}

using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class DocumentValidator : AbstractValidator<DocumentDTO>
    {
        public DocumentValidator()
        {
            RuleFor(t => t.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.Description)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.Path)
                .NotNull()
                .NotEmpty();
        }
    }
}

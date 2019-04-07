using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class TokenValidator : AbstractValidator<TokenDTO>
    {
        public TokenValidator()
        {
            RuleFor(t => t.AccessToken)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.RefreshToken)
                .NotNull()
                .NotEmpty();
        }
    }
}
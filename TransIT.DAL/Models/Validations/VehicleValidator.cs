using FluentValidation;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class VehicleValidator : AbstractValidator<VehicleDTO>
    {
        public VehicleValidator()
        {
            RuleFor(t => t.Brand)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.Model)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.Vincode)
                .NotNull()
                .NotEmpty();
            RuleFor(t => t.InventoryId)
                .NotNull();
            RuleFor(t => t.VehicleType)
                .NotNull();
            RuleFor(t => t.VehicleType.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.Validations
{
    public class LocationValidator : AbstractValidator<LocationDTO>
    {
        public LocationValidator()
        {
            RuleFor(t => t.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}

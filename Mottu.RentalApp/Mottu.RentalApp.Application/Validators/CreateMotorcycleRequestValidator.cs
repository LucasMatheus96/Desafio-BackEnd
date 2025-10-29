using FluentValidation;
using Mottu.RentalApp.Application.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.Validators
{
    public class CreateMotorcycleRequestValidator : AbstractValidator<CreateMotorcycleRequest>
    {
        public CreateMotorcycleRequestValidator()
        {
            RuleFor(x=> x.Year).GreaterThan(2000).WithMessage("Year must be a valid year.");
            RuleFor(x => x.Model).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Plate).NotEmpty().MaximumLength(16);
        }
    }
}

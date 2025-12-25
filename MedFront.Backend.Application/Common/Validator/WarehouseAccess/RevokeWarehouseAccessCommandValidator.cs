using FluentValidation;
using MedFront.Backend.Application.Services.WarehouseAccess.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Application.Common.Validator.WarehouseAccess
{
    public class RevokeWarehouseAccessCommandValidator : AbstractValidator<RevokeWarehouseAccessCommand>
    {
        public RevokeWarehouseAccessCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("WarehouseAccess Id is required.");
        }
    }
}

using FluentValidation;
using MedFront.Backend.Application.Services.WarehouseAccess.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Application.Common.Validator.WarehouseAccess
{
    public class GrantWarehouseAccessCommandValidator : AbstractValidator<GrantWarehouseAccessCommand>
    {
        public GrantWarehouseAccessCommandValidator()
        {
            RuleFor(x => x.Dto).NotNull();

            RuleFor(x => x.Dto.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.");

            RuleFor(x => x.Dto.WarehouseId)
                .NotEmpty()
                .WithMessage("WarehouseId is required.");
        }
    }
}

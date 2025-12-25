using FluentValidation;
using MedFront.Backend.Application.Services.Warehouse.Commands;

namespace MedFront.Backend.Application.Services.Warehouse.Validators
{
    public class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
    {
        public UpdateWarehouseCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Dto.Address)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Dto.TotalVolume)
                .GreaterThan(0);
        }
    }
}

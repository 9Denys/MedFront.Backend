using FluentValidation;
using MedFront.Backend.Application.Services.Warehouse.Commands;

namespace MedFront.Backend.Application.Services.Warehouse.Validators
{
    public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
    {
        public CreateWarehouseCommandValidator()
        {
            RuleFor(x => x.Dto.Address)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Dto.TotalVolume)
                .GreaterThan(0);
        }
    }
}

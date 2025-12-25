using FluentValidation;
using MedFront.Backend.Application.Services.Warehouse.Commands;

namespace MedFront.Backend.Application.Services.Warehouse.Validators
{
    public class DeleteWarehouseCommandValidator : AbstractValidator<DeleteWarehouseCommand>
    {
        public DeleteWarehouseCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}

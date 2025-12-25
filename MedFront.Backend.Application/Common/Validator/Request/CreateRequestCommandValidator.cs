using FluentValidation;
using MedFront.Backend.Application.Services.Request.Commands;

namespace MedFront.Backend.Application.Common.Validator.Request
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(x => x.Dto).NotNull();

            RuleFor(x => x.Dto.WarehouseId)
                .NotEmpty()
                .WithMessage("WarehouseId is required.");

            RuleFor(x => x.Dto.MedicationId)
                .NotEmpty()
                .WithMessage("MedicationId is required.");

            RuleFor(x => x.Dto.BoxQuantity)
                .GreaterThan(0)
                .WithMessage("BoxQuantity must be greater than 0.");

            RuleFor(x => x.Dto.Description)
                .MaximumLength(1000)
                .WithMessage("Description must be at most 1000 characters.");
        }
    }
}

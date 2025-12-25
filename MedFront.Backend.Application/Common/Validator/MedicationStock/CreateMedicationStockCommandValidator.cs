using FluentValidation;
using MedFront.Backend.Application.Services.MedicationStock.Commands;

namespace MedFront.Backend.Application.Common.Validator.MedicationStock
{
    public class CreateMedicationStockCommandValidator : AbstractValidator<CreateMedicationStockCommand>
    {
        public CreateMedicationStockCommandValidator()
        {
            RuleFor(x => x.Dto).NotNull();

            RuleFor(x => x.Dto.MedicationId)
                .NotEmpty().WithMessage("MedicationId is required.");

            RuleFor(x => x.Dto.WarehouseId)
                .NotEmpty().WithMessage("WarehouseId is required.");

            RuleFor(x => x.Dto.BoxQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("BoxQuantity must be >= 0.");

            RuleFor(x => x.Dto.StockNorm)
                .GreaterThanOrEqualTo(0).WithMessage("StockNorm must be >= 0.");

            RuleFor(x => x.Dto.ExpirationDate)
                .Must(d => d is null || d.Value >= DateOnly.FromDateTime(DateTime.UtcNow.Date))
                .WithMessage("ExpirationDate cannot be in the past.");
        }
    }
}

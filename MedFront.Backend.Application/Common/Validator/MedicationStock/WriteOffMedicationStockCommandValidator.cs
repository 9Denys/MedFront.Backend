using FluentValidation;
using MedFront.Backend.Application.Services.MedicationStock.Commands;

namespace MedFront.Backend.Application.Common.Validator.MedicationStock
{
    public class WriteOffMedicationStockCommandValidator : AbstractValidator<WriteOffMedicationStockCommand>
    {
        public WriteOffMedicationStockCommandValidator()
        {
            RuleFor(x => x.StockId)
                .NotEmpty().WithMessage("StockId is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be > 0.");
        }
    }
}

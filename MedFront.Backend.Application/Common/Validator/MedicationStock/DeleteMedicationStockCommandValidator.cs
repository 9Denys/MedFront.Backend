using FluentValidation;
using MedFront.Backend.Application.Services.MedicationStock.Commands;

namespace MedFront.Backend.Application.Common.Validator.MedicationStock
{
    public class DeleteMedicationStockCommandValidator : AbstractValidator<DeleteMedicationStockCommand>
    {
        public DeleteMedicationStockCommandValidator()
        {
            RuleFor(x => x.StockId)
                .NotEmpty().WithMessage("StockId is required.");
        }
    }
}

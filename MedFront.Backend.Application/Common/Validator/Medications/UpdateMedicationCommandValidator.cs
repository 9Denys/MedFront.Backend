using FluentValidation;
using MedFront.Backend.Application.Services.Medication.Commands;

namespace MedFront.Backend.Application.Services.Medication.Validators
{
    public class UpdateMedicationCommandValidator
        : AbstractValidator<UpdateMedicationCommand>
    {
        public UpdateMedicationCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Dto.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Dto.SKU)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Dto.Category)
                .MaximumLength(50);

            RuleFor(x => x.Dto.PackageVolume)
                .GreaterThan(0);
        }
    }
}

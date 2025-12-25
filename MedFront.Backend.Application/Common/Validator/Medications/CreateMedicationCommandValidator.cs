using FluentValidation;
using MedFront.Backend.Application.Services.Medication.Commands;

namespace MedFront.Backend.Application.Services.Medication.Validators
{
    public class CreateMedicationCommandValidator
        : AbstractValidator<CreateMedicationCommand>
    {
        public CreateMedicationCommandValidator()
        {
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

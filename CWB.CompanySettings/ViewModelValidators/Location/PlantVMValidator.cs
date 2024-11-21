using CWB.CompanySettings.ViewModels.Location;
using CWB.CompanySettings.ViewModelValidatorsMessage.Location;
using FluentValidation;

namespace CWB.CompanySettings.ViewModelValidators.Location
{
    public class PlantVMValidator : AbstractValidator<PlantVM>
    {
        public PlantVMValidator()
        {

            RuleFor(v => v.Name)
                   .NotEmpty().WithMessage(PlantVMValidatorMessage.EmptyPlant);
            RuleFor(v => v.Address)
                   .NotEmpty().WithMessage(PlantVMValidatorMessage.EmptyAddress);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(PlantVMValidatorMessage.EmptyTenantId);
        }

    }
}
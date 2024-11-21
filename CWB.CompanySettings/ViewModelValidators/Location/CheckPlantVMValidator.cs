using CWB.CompanySettings.ViewModels.Location;
using CWB.CompanySettings.ViewModelValidatorsMessage.Location;
using FluentValidation;

namespace CWB.CompanySettings.ViewModelValidators.Location
{
    public class CheckPlantVMValidator : AbstractValidator<CheckPlantVM>
    {
        public CheckPlantVMValidator()
        {

            RuleFor(v => v.Name)
                   .NotEmpty().WithMessage(CheckPlantVMValidatorMessage.EmptyPlant);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(CheckPlantVMValidatorMessage.EmptyTenantId);
        }

    }
}

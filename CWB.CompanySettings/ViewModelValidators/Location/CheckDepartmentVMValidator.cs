using CWB.CompanySettings.ViewModels.Location;
using CWB.CompanySettings.ViewModelValidatorsMessage.Location;
using FluentValidation;

namespace CWB.CompanySettings.ViewModelValidators.Location
{
    public class CheckDepartmentVMValidator : AbstractValidator<CheckDepartmentVM>
    {
        public CheckDepartmentVMValidator()
        {
            RuleFor(v => v.Name)
                   .NotEmpty().WithMessage(CheckDepartmentVMValidatorMessage.EmptyDepartment);
            RuleFor(v => v.PlantId)
                   .NotEmpty().WithMessage(CheckDepartmentVMValidatorMessage.EmptyPlantId);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(CheckDepartmentVMValidatorMessage.EmptyTenantId);
        }

    }
}
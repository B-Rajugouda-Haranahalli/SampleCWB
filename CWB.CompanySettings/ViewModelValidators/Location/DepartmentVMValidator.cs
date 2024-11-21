using CWB.CompanySettings.ViewModels.Location;
using CWB.CompanySettings.ViewModelValidatorsMessage.Location;
using FluentValidation;

namespace CWB.CompanySettings.ViewModelValidators.Location
{
    public class DepartmentVMValidator : AbstractValidator<ShopDepartmentVM>
    {
        public DepartmentVMValidator()
        {
            RuleFor(v => v.Name)
                   .NotEmpty().WithMessage(DepartmentVMValidatorMessage.EmptyDepartment);
            RuleFor(v => v.NoOfShifts)
                   .NotEmpty().WithMessage(DepartmentVMValidatorMessage.EmptyNoOfShifts);
            RuleFor(v => v.PlantId)
                   .NotEmpty().WithMessage(DepartmentVMValidatorMessage.EmptyPlantId);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(DepartmentVMValidatorMessage.EmptyTenantId);
        }

    }
}
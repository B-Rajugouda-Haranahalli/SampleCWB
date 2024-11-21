using CWB.Simulation.ViewModels;
using CWB.Simulation.ViewModelValidatorsMessage;
using FluentValidation;

namespace CWB.Simulation.ViewModelValidators
{
    public class ShopDepartmentVMValidator : AbstractValidator<ShopDepartmentVM>
    {
        public ShopDepartmentVMValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(ShopDepartmentVMValidatorMessage.EmptyName);

            RuleFor(c => c.NoOfShifts)
                .NotEmpty().WithMessage(WorkDayMasterVMValidatorMessage.EmptyNoOfShifts);
        }
    }
}

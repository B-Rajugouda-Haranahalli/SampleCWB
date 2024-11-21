using CWB.Masters.ViewModels.Machines;
using CWB.Masters.ViewModelValidatorsMessage.Machines;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.Machines
{
    public class CheckMachineVMValidator : AbstractValidator<CheckMachineVM>
    {
        public CheckMachineVMValidator()
        {
            RuleFor(v => v.CompareKey)
                   .NotEmpty().WithMessage(CheckMachineVMValidatorMessage.EmptyCompareKey);
            RuleFor(v => v.CompareValue)
                  .NotEmpty().WithMessage(CheckMachineVMValidatorMessage.EmptyCompareValue);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(CheckMachineVMValidatorMessage.EmptyTenantId);
        }
    }
}
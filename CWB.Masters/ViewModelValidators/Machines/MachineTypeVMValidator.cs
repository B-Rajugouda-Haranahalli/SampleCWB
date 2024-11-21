using CWB.Masters.ViewModels.Machines;
using CWB.Masters.ViewModelValidatorsMessage.Machines;
using FluentValidation;

namespace CWB.Masters.ViewModelValidators.Machines
{
    public class MachineTypeVMValidator : AbstractValidator<MachineTypeVM>
    {
        public MachineTypeVMValidator()
        {
            RuleFor(v => v.MachineTypeName)
                   .NotEmpty().WithMessage(MachineTypeVMValidatorMessage.EmptyMachineType);
            RuleFor(v => v.TenantId)
                  .NotEmpty().WithMessage(MachineTypeVMValidatorMessage.EmptyTenantId);
        }
    }
}